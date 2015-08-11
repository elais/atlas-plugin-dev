# Atlassian Connect .NET

Atlassian Connect .NET is a framework to build add-ons for Atlassian products using
[Atlassian Connect](http://connect.atlassian.com). Using add-ons you can extend with a new 
feature(s), integrate with other systems, and otherwise customize your Atlassian product. 

## Licensing

This is licensed under Apache 2.0, see LICENSE.txt for more details. 

## Getting started

Install the `Atlassian.Connect` package via NuGet to an ASP.NET MVC app. Atlassian.Connect does require
.NET 4.5. 

### Considerations

The default of using IIS Express can be a challenge, as a JIRA instance would not be able to make requests
to your add-on without work to reconfigure IIS Express. We suggest that you use IIS instead of IIS
Express to host your add-on locally. Running against local IIS requires administrator privileges; [automating
"Run as Administrator"](http://stackoverflow.com/a/12859334/396746) may help local development.

### Adding Connect JavaScript bridge to a page in your add-on

A helper exists to add the `<script>` include for all.js, simple add `@Html.IncludeConnectJs()` in
you Razor layout and the helper handles using the query string to find the right URL for inclusion. 

### Default installed webhook

The installed webhook URL must be mapped to a controller action. A default controller action for
this would be. 

````
[HttpPost]
public ActionResult InstalledCallback()
{
    SecretKeyPersister.SaveSecretKey(Request);

    return Content(String.Empty);
}
````

`SecretKeyPersister` is a simple storage system backed by Entity Framework to persist details about
the instances that are shared with your add-on. To use this you will need to have a Connect String
registered in your web.config.

````
<add name="SharedSecretsConnectionString" connectionString="Data Source=|DataDirectory|\Secrets.sdf" providerName="System.Data.SqlServerCe.4.0" />
````

You can easily replace this in the future. Implement
`ISecretKeyProvider` and set the `SecretKeyProviderFactory.Factory` property to a function that 
returns an instance of your implementation. 

### Validating a call with JWT

Any controller or action can have a filter attribute applied, `[JwtAuthentication]`, that will 
validate the JWT claims and return an unauthorized result if the validation fails. 

### Making a REST API call from a controller action

There is an extention method on the controller's `Request` property that creates an 
`HttpClient` you can use to make REST API calls back to the product instance. This handles all
the JWT signing and base URL setting for you. 

````
// create the HttpClient, setup for Connect
HttpClient client = Request.CreateConnectHttpClient("com.example.myaddon");

// make a rest call, just need the relative URL
var response = client.GetAsync("rest/api/latest/project").Result;
// get the body of the response, normally containing the result of the call
var results = response.Content.ReadAsStringAsync().Result;
````

## Reporting Issues

Please use the [Atlassian Connect project](https://ecosystem.atlassian.net/browse/AC)
to report issues, bugs, or make feature requests.

## Contributing

We welcome minor bug fixes and they may be accepted via a pull request. Substantial patches will
require a [contributor license agreement](https://developer.atlassian.com/about/atlassian-contributor-license-agreement) on file.