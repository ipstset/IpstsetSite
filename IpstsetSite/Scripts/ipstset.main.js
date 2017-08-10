var emSubNav = {};
var popGames = {};
var popAbout = {};
var emSubNavPlay = {};

var games = [];
var aboutsMe = [];

$(document).ready(function () {
    init();


    //test...add games
    var game = {};
    game.title = "Letter Munch";
    game.body = '<p>Munch...Spell...Score!</p><p>How large is your vocabulary? Capture the falling letters to create words. Use double and triple-score tiles and chain multiple bonuses to keep time from expiring and get the high score.</p><div>Available now on the <a class="marketplace" href="http://marketplace.xbox.com/en-US/Product/Letter-Munch/66acd000-77fe-1000-9115-d80258550af7">Xbox Live Marketplace</a></div><p>Visit game site at <a href="http://www.lettermunch.com" title="Visit game site at lettermunch.com">www.lettermunch.com</a>.</p>';
    game.image = "boxart_letter_munch.png";
    games.push(game);

    game = {};
    game.title = "Earth's End";
    game.body = "<p>The invasion has begun.  Take control of Earth's best soldiers and battle endless waves of aliens and their monstrous walkers while protecting the last of mankind.  Can you prevent the earth's end?</p>Features:<ul><li>2D action-platformer</li><li>2 player co-op</li><li>Dozens of in-game medals to earn</li></ul><p>Expected release date Fall 2012 on Xbox Live Indie Games.</p>";
    game.image = "boxart_none.png";
    games.push(game);

//    var about = {};
//    about.body = 'I ran <a href="http://www.baa.org/races/boston-marathon.aspx">this</a> twice';
//    aboutsMe.push(about);

//    about = {};
//    about.body = 'sometimes I watch <a href="http://www.fox.com/hellskitchen/">this</a>.';
//    aboutsMe.push(about);

});

init = function () {
    emSubNav = $(".navigation-sub");
    popGames = new ipstset_modal.Modal($('#pop-games'));
    popAbout = new ipstset_modal.Modal($('#pop-about'));
    emSubNavPlay = $(".navigation-sub-play");
};

showGames = function() {
    emSubNavPlay.hide();
    //todo:each in games...fade in, pause fade next

    emSubNav.fadeToggle(600, 'linear');
};

showDetail = function (index) {
    var game = games[index];
    popGames.show();
    $(".pop-games-header").html(game.title);
    $(".pop-games-left").html(game.body);
    $(".pop-games-right img").attr('src','/Content/images/' + game.image);
    
};

showAbout = function () {
    emSubNav.hide();
    emSubNavPlay.hide();    

    var about1 = aboutsMe[0];
    var about2 = aboutsMe[1];

    popAbout.show();
    $("#about-me-1").html(about1.body);
    $("#about-me-2").html(about2.body);

};

showPlay = function () {
    emSubNav.hide();
    emSubNavPlay.fadeToggle(600, 'linear');
};