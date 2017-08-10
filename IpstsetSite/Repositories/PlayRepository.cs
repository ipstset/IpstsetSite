using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IpstsetSite.Models;
using System.Data.SqlClient;

namespace IpstsetSite.Repositories
{
    public class PlayRepository:IPlayRepository
    {
        private string _connection;

        public PlayRepository(string connection)
        {
            _connection = connection;
        }
        public HangmanViewModel GetHangmanGame(int id)
        {
            var model = new HangmanViewModel();

            var con = new SqlConnection(_connection);
            var cmd = new SqlCommand("play_GetHangmanGame", con);
            cmd.Parameters.Add(new SqlParameter("@gameID", id));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (con)
            {
                con.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    model.Id = Convert.ToInt32(reader["GameID"]);
                    model.RemainingGuesses = Convert.ToInt32(reader["Remaining"]);
                    model.GameOver = Convert.ToBoolean(reader["GameOver"]);
                    model.GameType = HangmanGameTypeFactory.Create(Convert.ToInt32(reader["GameTypeID"]));
                    model.Clue = reader["Clue"].ToString();

                    //create characters from answer
                    var answer = reader["Answer"].ToString();
                    model.Characters  =new List<HangmanCharacter>();
                    foreach (char c in answer)
                    {
                        if(Char.IsLetterOrDigit(c))
                        {
                            model.Characters.Add(new HangmanCharacter {Tiled = true, Value = c.ToString()});
                        }
                        else
                        {
                            model.Characters.Add(new HangmanCharacter { Tiled = false, Value = c.ToString(),Visible = true});
                        }
                    }

                    //guessed
                    string[] guessed;
                    model.Guessed = new List<string>();
                    if(!String.IsNullOrWhiteSpace(reader["Guessed"].ToString()))
                    {
                        guessed = reader["Guessed"].ToString().Split(',');

                        for (int i = 0; i < guessed.Length; i++)
                        {
                            int gIndex = i;
                            var characters = model.Characters.Where(c => c.Value.ToLower() == guessed[gIndex].ToLower()).ToList();
                            foreach (var c in characters)
                                c.Visible = true;

                            model.Guessed.Add(guessed[gIndex]);
                        }
                    }

                    
                    
                }

                con.Close();
            }

            return model;


        }

        public bool ActiveGameExists(int id, string authenticationToken)
        {
            bool exists;
            try
            {
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("play_ActiveGameExists", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@gameID", id));
                cmd.Parameters.Add(new SqlParameter("@authToken", authenticationToken));

                using (con)
                {
                    con.Open();
                    exists = Convert.ToBoolean(cmd.ExecuteScalar());
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return exists;
        }





        public bool UpdateGameAuthentication(int gameId, string authenticationToken)
        {
            try
            {
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("play_UpdateGameAuthToken", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@gameID", gameId));
                cmd.Parameters.Add(new SqlParameter("@authToken", authenticationToken));

                using (con)
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            

            return true;
        }



        public int CreateHangmanGame(HangmanAnswer answer, int gameTypeId, int guesses, string userIdentityToken)
        {
            int id;
            try
            {
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("play_CreateHangmanGame", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@answer", answer.Answer));
                cmd.Parameters.Add(new SqlParameter("@clue", answer.Clue));
                cmd.Parameters.Add(new SqlParameter("@gameTypeID", gameTypeId));
                cmd.Parameters.Add(new SqlParameter("@numberOfGuesses", guesses));
                cmd.Parameters.Add(new SqlParameter("@userIdentityToken", userIdentityToken));
                using (con)
                {
                    con.Open();
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id;

        }





        public void UpdateHangmanGame(HangmanViewModel game)
        {
            var guessed = string.Empty;
            foreach (var g in game.Guessed)
                guessed += g + ",";

            if(!String.IsNullOrEmpty(guessed))
            {
                if(guessed.EndsWith(","))
                    guessed = guessed.Remove(guessed.Length-1);
            }

            try
            {
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("play_UpdateGame", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@gameID", game.Id));
                cmd.Parameters.Add(new SqlParameter("@remaining", game.RemainingGuesses));
                cmd.Parameters.Add(new SqlParameter("@guessed", guessed));
                cmd.Parameters.Add(new SqlParameter("@gameOver", game.GameOver));

                using (con)
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            


            //
        }


        public HangmanAnswer GetRandomAnswer(int gameType)
            {
                var answer = new HangmanAnswer();
                try
                {
                    var con = new SqlConnection(_connection);
                    var cmd = new SqlCommand("play_GetRandomAnswer", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@gameTypeID", gameType));
                    using (con)
                    {
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            answer.Answer = reader["Answer"].ToString();
                            answer.Clue = reader["Clue"].ToString();
                        }
                        con.Close();
                    }

                    return answer;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        public bool HasWonXboxCode(string identityToken)
        {
            try
            {
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("play_HasWonXboxCode", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdentityToken", identityToken));

                var results = 0;
                using (con)
                {
                    con.Open();
                    results = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }

                return results > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}