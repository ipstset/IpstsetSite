using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using IpstsetSite.Models;

namespace IpstsetSite.Repositories
{
    public class RedirectRepository: IRedirectRepository
    {
        private string _connection;
        public RedirectRepository(string connection)
        {
            _connection = connection;
        }

        public SiteRedirect GetSiteRedirect(string siteText)
        {
            
            try
            {
                var redirect = new SiteRedirect();
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("redirect_GetSiteByText", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Text", siteText));
                using (con)
                {
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        redirect.Id = Convert.ToInt32(reader["SiteRedirectID"].ToString());
                        redirect.Text = reader["Text"].ToString();
                        redirect.Url = reader["Url"].ToString();
                    }
                    con.Close();
                }

                return redirect.Id == 0 ? null : redirect;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SiteRedirect> GetAll()
        {

            try
            {
                var redirects = new List<SiteRedirect>();
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("redirect_GetSites", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (con)
                {
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        redirects.Add(new SiteRedirect
                                          {
                                               Id = Convert.ToInt32(reader["SiteRedirectID"].ToString()),
                                               Text = reader["Text"].ToString(),
                                               Url = reader["Url"].ToString(),
                                               DateCreated = Convert.ToDateTime(reader["DateCreated"])
                                          });

                    }
                    con.Close();
                }

                return redirects;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Save(string text, string url)
        {
            try
            {
                var redirect = new SiteRedirect();
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("redirect_Save", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Text", text));
                cmd.Parameters.Add(new SqlParameter("@Url", url));
                cmd.Parameters.Add(new SqlParameter("@DateCreated", DateTime.Now));

                using (con)
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var redirect = new SiteRedirect();
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("redirect_Delete", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteRedirectID", id));

                using (con)
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}