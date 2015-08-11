# Atlassian Connect .NET Changelog

## v1.0.1, 2015-05-14

* Added tests for `DateTime` conversion to Unix timestamp
* Added better handling for `dynamic` objects when creating the descriptor
* Added `[JsonProperty]` attributes to `dynamic` object properties in prep for changes coming in ASP.NET MVC 6
* Changed storage selection of secret key data to use `ClientKey` instead of `BaseUrl`
* Added `sub` claim to out going JWT signatures
* Removed some crud from repo

## v1.0, 2015-02-24

* Breaking change from v0.2
  * `ConnectDescriptor` no longer contains apiVersion property. Because `ConnectDescriptor` is a dynamic
    object, you can still directly set the parameter (but not in a object initializer)

## v0.2, 2015-01-28

* Better handling of JWT signatures