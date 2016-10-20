snapChallenge
    .config(function($stateProvider, $urlRouterProvider)
    {
        $urlRouterProvider.otherwise("/home");

        $stateProvider.state("home", {
            url: "/home",
            templateUrl: "partials/dashboard.html"
        });
    });
