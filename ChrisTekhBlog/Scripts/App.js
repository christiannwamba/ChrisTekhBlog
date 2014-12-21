var AdminApp = angular.module("AdminApp", ["ui.bootstrap", "ui.tinymce"])

AdminApp.controller("ManagePostCtrl", function ($scope, $http, $modal) {
    $scope.tinymceOptions = {
        resize: false,
        height: 300,
        plugins: 'print textcolor advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking save table contextmenu directionality emoticons template paste textcolor',
        toolbar: "undo redo styleselect  | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | l      ink image | print preview media fullpage | forecolor backcolor emoticons sizeselect | bold italic | fontselect |  fontsizeselect", file_browser_callback: RoxyFileBrowser

    };
    $scope.ToJavaScriptDate = function (value) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var dt = new Date(parseFloat(results[1]));
        return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
    }
    $http.get("/Json/Posts").success(function (data) {
        $scope.posts = data;
       
    })
    $http.get("/Json/Categories").success(function (data) {
        $scope.categories = data;
    })
    //page
    $scope.itemsPerPage = 10; $scope.currentPage = 0; $scope.items = []; for (var i = 0; i < 50; i++) { $scope.items.push({ id: i, name: "name " + i, description: "description " + i }); } $scope.prevPage = function () { if ($scope.currentPage > 0) { $scope.currentPage--; } }; $scope.prevPageDisabled = function () { return $scope.currentPage === 0 ? "disabled" : ""; }; $scope.pageCount = function () { return Math.ceil($scope.posts.length / $scope.itemsPerPage) - 1; }; $scope.nextPage = function () { if ($scope.currentPage < $scope.pageCount()) { $scope.currentPage++; } }; $scope.nextPageDisabled = function () { return $scope.currentPage === $scope.pageCount() ? "disabled" : ""; };

    $scope.createPost = function () {
        var Post = { Title: $scope.Title, ShortDescription: $scope.ShortDescription, Description: $scope.Description, Meta: $scope.Meta, UrlSlug: $scope.UrlSlug, Published: $scope.Published, CategoryId: $scope.CategoryId };
        var Post2 = { Title: "s", ShortDescription: "s", Description: "a", Meta:"s", UrlSlug: "s", Published: "true", CategoryId: "1" };
        $http.post("/Admin/CreatePost", Post).success(function () {
            $http.get("/Json/Posts").success(function (data) {
                $scope.posts = data;
            })
        })
    }
    $scope.editPost = function (id) {
        $http.get("/Json/Post/"+id).success(function (data) {
            $scope.Title = data.Title;
            $scope.ShortDescription = data.ShortDescription;
            $scope.Description = data.Description;
            $scope.Meta = data.Meta;
            $scope.UrlSlug = data.UrlSlug;
            $scope.Published = data.Published;
            $scope.CategoryId = data.CategoryId;
            $scope.PostId = data.Id;
        })
    }
    $scope.updatePost = function () {
        var Post = { Title: $scope.Title, ShortDescription: $scope.ShortDescription, Description: $scope.Description, Meta: $scope.Meta, UrlSlug: $scope.UrlSlug, Published: $scope.Published, CategoryId: $scope.CategoryId, Id: $scope.PostId };
        $http.post("/Admin/UpdatePost", Post).success(function () {
            $http.get("/Json/Posts").success(function (data) {
                $scope.posts = data;
            })
        })
    }

    $scope.deletePost = function (id) {
        $http.get("/Admin/DeletePost/"+id).success(function () {
            $http.get("/Json/Posts").success(function (data) {
                $scope.posts = data;
            })
        })
    }

   
})

AdminApp.controller("ManageCatCtrl", function ($scope, $http) {
  
    $scope.itemsPerPage = 10; $scope.currentPage = 0; $scope.items = []; for (var i = 0; i < 50; i++) { $scope.items.push({ id: i, name: "name " + i, description: "description " + i }); } $scope.prevPage = function () { if ($scope.currentPage > 0) { $scope.currentPage--; } }; $scope.prevPageDisabled = function () { return $scope.currentPage === 0 ? "disabled" : ""; }; $scope.pageCount = function () { return Math.ceil($scope.posts.length / $scope.itemsPerPage) - 1; }; $scope.nextPage = function () { if ($scope.currentPage < $scope.pageCount()) { $scope.currentPage++; } }; $scope.nextPageDisabled = function () { return $scope.currentPage === $scope.pageCount() ? "disabled" : ""; };
    $http.get("/Json/Categories").success(function (data) {
        $scope.categories = data;
    })

    $scope.createCategory = function () {
        var Category = { Name: $scope.Name, Description: $scope.Description, UrlSlug: $scope.UrlSlug };
        $http.post("/Admin/CreateCategory", Category).success(function () {
            $http.get("/Json/Categories").success(function (data) {
                $scope.categories = data;
            })
        })
    }
    $scope.editCategory = function (id) {
        $http.get("/Json/Category/" + id).success(function (data) {
            $scope.Name = data.Name;
            $scope.Description = data.Description;
            $scope.UrlSlug = data.UrlSlug;
            $scope.CategoryId = data.Id;
        })
    }
    $scope.updateCategory = function () {
        var Category = { Name: $scope.Name, Description: $scope.Description, UrlSlug: $scope.UrlSlug, Id: $scope.CategoryId };
        $http.post("/Admin/UpdateCategory", Category).success(function () {
            $http.get("/Json/Categories").success(function (data) {
                $scope.categories = data;
            })
        })
    }

    $scope.deleteCategory = function (id) {
        $http.get("/Admin/DeleteCategory/" + id).success(function () {
            $http.get("/Json/Categories").success(function (data) {
                $scope.categories = data;
            })
        })
    }
})

AdminApp.filter('offset', function () { return function (input, start) { start = parseInt(start, 10); return input.slice(start); }; });
function RoxyFileBrowser(field_name, url, type, win) {
    var roxyFileman = '/fileman/index.html';
    if (roxyFileman.indexOf("?") < 0) {
        roxyFileman += "?type=" + type;
    }
    else {
        roxyFileman += "&type=" + type;
    }
    roxyFileman += '&input=' + field_name + '&value=' + document.getElementById(field_name).value;
    if (tinyMCE.activeEditor.settings.language) {
        roxyFileman += '&langCode=' + tinyMCE.activeEditor.settings.language;
    }
    tinyMCE.activeEditor.windowManager.open({
        file: roxyFileman,
        title: 'Roxy Fileman',
        width: 850,
        height: 650,
        resizable: "yes",
        plugins: "media",
        inline: "yes",
        close_previous: "no"
    }, { window: win, input: field_name });
    return false;
}