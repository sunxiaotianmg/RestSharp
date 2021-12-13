using System.Net;
using RestSharp.Serializers;
using RestSharp.Serializers.Xml;

namespace RestSharp;

public interface IRestRequest {

    /// <summary>
    /// Adds a file to the Files collection to be included with a POST or PUT request
    /// (other methods do not support file uploads).
    /// </summary>
    /// <param name="name">The parameter name to use in the request</param>
    /// <param name="path">Full path to file to upload</param>
    /// <param name="contentType">The MIME type of the file to upload</param>
    /// <returns>This request</returns>
    IRestRequest AddFile(string name, string path, string? contentType = null);

    /// <summary>
    /// Adds the bytes to the Files collection with the specified file name and content type
    /// </summary>
    /// <param name="name">The parameter name to use in the request</param>
    /// <param name="bytes">The file data</param>
    /// <param name="fileName">The file name to use for the uploaded file</param>
    /// <param name="contentType">The MIME type of the file to upload</param>
    /// <returns>This request</returns>
    IRestRequest AddFile(string name, byte[] bytes, string fileName, string? contentType = null);

    /// <summary>
    /// Adds the bytes to the Files collection with the specified file name and content type
    /// </summary>
    /// <param name="name">The parameter name to use in the request</param>
    /// <param name="getFile">A function that returns the stream. Should NOT close the stream.</param>
    /// <param name="fileName">The file name to use for the uploaded file</param>
    /// <param name="contentLength">The length (in bytes) of the file content.</param>
    /// <param name="contentType">The MIME type of the file to upload</param>
    /// <returns>This request</returns>
    IRestRequest AddFile(string name, Func<Stream> getFile, string fileName, long contentLength, string? contentType = null);

    /// <summary>
    /// Add bytes to the Files collection as if it was a file of specific type
    /// </summary>
    /// <param name="name">A form parameter name</param>
    /// <param name="bytes">The file data</param>
    /// <param name="filename">The file name to use for the uploaded file</param>
    /// <param name="contentType">Specific content type. Es: application/x-gzip </param>
    /// <returns></returns>
    IRestRequest AddFileBytes(string name, byte[] bytes, string filename, string contentType = "application/x-gzip");

    /// <summary>
    /// Serializes obj to format specified by RequestFormat, but passes XmlNamespace if using the default XmlSerializer
    /// The default format is XML. Change RequestFormat if you wish to use a different serialization format.
    /// </summary>
    /// <param name="obj">The object to serialize</param>
    /// <param name="xmlNamespace">The XML namespace to use when serializing</param>
    /// <returns>This request</returns>
    IRestRequest AddBody(object obj, string xmlNamespace);

    /// <summary>
    /// Serializes obj to data format specified by RequestFormat and adds it to the request body.
    /// The default format is XML. Change RequestFormat if you wish to use a different serialization format.
    /// </summary>
    /// <param name="obj">The object to serialize</param>
    /// <returns>This request</returns>
    IRestRequest AddBody(object obj);

    /// <summary>
    /// Instructs RestSharp to send a given object in the request body, serialized as JSON.
    /// </summary>
    /// <param name="obj">The object to serialize</param>
    /// <returns>This request</returns>
    IRestRequest AddJsonBody(object obj);

    /// <summary>
    /// Instructs RestSharp to send a given object in the request body, serialized as JSON.
    /// Allows specifying a custom content type. Usually, this method is used to support PATCH
    /// requests that require application/json-patch+json content type.
    /// </summary>
    /// <param name="obj">The object to serialize</param>
    /// <param name="contentType">Custom content type to override the default application/json</param>
    /// <returns>This request</returns>
    IRestRequest AddJsonBody(object obj, string contentType);

    /// <summary>
    /// Instructs RestSharp to send a given object in the request body, serialized as XML.
    /// </summary>
    /// <param name="obj">The object to serialize</param>
    /// <returns>This request</returns>
    IRestRequest AddXmlBody(object obj);

    /// <summary>
    /// Instructs RestSharp to send a given object in the request body, serialized as XML
    /// but passes XmlNamespace if using the default XmlSerializer.
    /// </summary>
    /// <param name="obj">The object to serialize</param>
    /// <param name="xmlNamespace">The XML namespace to use when serializing</param>
    /// <returns>This request</returns>
    IRestRequest AddXmlBody(object obj, string xmlNamespace);

    /// <summary>
    /// Calls AddParameter() for all public, readable properties specified in the includedProperties list
    /// </summary>
    /// <example>
    /// request.AddObject(product, "ProductId", "Price", ...);
    /// </example>
    /// <param name="obj">The object with properties to add as parameters</param>
    /// <param name="includedProperties">The names of the properties to include</param>
    /// <returns>This request</returns>
    IRestRequest AddObject(object obj, params string[] includedProperties);

    /// <summary>
    /// Calls AddParameter() for all public, readable properties of obj
    /// </summary>
    /// <param name="obj">The object with properties to add as parameters</param>
    /// <returns>This request</returns>
    IRestRequest AddObject(object obj);

    /// <summary>
    /// Add the parameter to the request
    /// </summary>
    /// <param name="p">Parameter to add</param>
    /// <returns></returns>
    IRestRequest AddParameter(Parameter p);

    /// <summary>
    /// Adds a parameter to the request. There are five types of parameters:
    /// - GetOrPost: Either a QueryString value or encoded form value based on method
    /// - HttpHeader: Adds the name/value pair to the HTTP request's Headers collection
    /// - UrlSegment: Inserted into URL if there is a matching url token e.g. {AccountId}
    /// - Cookie: Adds the name/value pair to the HTTP request's Cookies collection
    /// - RequestBody: Used by AddBody() (not recommended to use directly)
    /// </summary>
    /// <param name="name">Name of the parameter</param>
    /// <param name="value">Value of the parameter</param>
    /// <param name="type">The type of parameter to add</param>
    /// <returns>This request</returns>
    IRestRequest AddParameter(string name, object value, ParameterType type);

    /// <summary>
    /// Adds a parameter to the request. There are five types of parameters:
    /// - GetOrPost: Either a QueryString value or encoded form value based on method
    /// - HttpHeader: Adds the name/value pair to the HTTP request's Headers collection
    /// - UrlSegment: Inserted into URL if there is a matching url token e.g. {AccountId}
    /// - Cookie: Adds the name/value pair to the HTTP request's Cookies collection
    /// - RequestBody: Used by AddBody() (not recommended to use directly)
    /// </summary>
    /// <param name="name">Name of the parameter</param>
    /// <param name="value">Value of the parameter</param>
    /// <param name="contentType">Content-Type of the parameter</param>
    /// <param name="type">The type of parameter to add</param>
    /// <returns>This request</returns>
    IRestRequest AddParameter(string name, object value, string contentType, ParameterType type);

    /// <summary>
    /// Adds a parameter to the request or updates it with the given argument, if the parameter already exists in the
    /// request.
    /// </summary>
    /// <param name="parameter">Parameter to add</param>
    /// <returns></returns>
    IRestRequest AddOrUpdateParameter(Parameter parameter);

    /// <summary>
    /// Add or update parameters to the request
    /// </summary>
    /// <param name="parameters">Collection of parameters to add</param>
    /// <returns></returns>
    IRestRequest AddOrUpdateParameters(IEnumerable<Parameter> parameters);

    /// <summary>
    /// Adds a HTTP parameter to the request (QueryString for GET, DELETE, OPTIONS and HEAD; Encoded form for POST and PUT)
    /// </summary>
    /// <param name="name">Name of the parameter</param>
    /// <param name="value">Value of the parameter</param>
    /// <returns>This request</returns>
    IRestRequest AddOrUpdateParameter(string name, object value);

    /// <summary>
    /// Adds a parameter to the request. There are five types of parameters:
    /// - GetOrPost: Either a QueryString value or encoded form value based on method
    /// - HttpHeader: Adds the name/value pair to the HTTP request Headers collection
    /// - UrlSegment: Inserted into URL if there is a matching url token e.g. {AccountId}
    /// - Cookie: Adds the name/value pair to the HTTP request Cookies collection
    /// - RequestBody: Used by AddBody() (not recommended to use directly)
    /// </summary>
    /// <param name="name">Name of the parameter</param>
    /// <param name="value">Value of the parameter</param>
    /// <param name="type">The type of parameter to add</param>
    /// <returns>This request</returns>
    IRestRequest AddOrUpdateParameter(string name, object value, ParameterType type);

    /// <summary>
    /// Adds a parameter to the request. There are five types of parameters:
    /// - GetOrPost: Either a QueryString value or encoded form value based on method
    /// - HttpHeader: Adds the name/value pair to the HTTP request Headers collection
    /// - UrlSegment: Inserted into URL if there is a matching url token e.g. {AccountId}
    /// - Cookie: Adds the name/value pair to the HTTP request Cookies collection
    /// - RequestBody: Used by AddBody() (not recommended to use directly)
    /// </summary>
    /// <param name="name">Name of the parameter</param>
    /// <param name="value">Value of the parameter</param>
    /// <param name="contentType">Content-Type of the parameter</param>
    /// <param name="type">The type of parameter to add</param>
    /// <returns>This request</returns>
    IRestRequest AddOrUpdateParameter(string name, object value, string contentType, ParameterType type);

    /// <summary>
    /// Shortcut to AddParameter(name, value, HttpHeader) overload
    /// </summary>
    /// <param name="name">Name of the header to add</param>
    /// <param name="value">Value of the header to add</param>
    /// <returns>This request</returns>
    IRestRequest AddHeader(string name, string value);

    /// <summary>
    /// Shortcut to AddOrUpdateParameter(name, value, HttpHeader) overload
    /// </summary>
    /// <param name="name">Name of the header to add or update</param>
    /// <param name="value">Value of the header to add or update</param>
    /// <returns>This request</returns>
    IRestRequest AddOrUpdateHeader(string name, string value);

    /// <summary>
    /// Uses AddHeader(name, value) in a convenient way to pass
    /// in multiple headers at once.
    /// </summary>
    /// <param name="headers">Key/Value pairs containing the name: value of the headers</param>
    /// <returns>This request</returns>
    IRestRequest AddHeaders(ICollection<KeyValuePair<string, string>> headers);

    /// <summary>
    /// Uses AddOrUpdateHeader(name, value) in a convenient way to pass
    /// in multiple headers at once.
    /// </summary>
    /// <param name="headers">Key/Value pairs containing the name: value of the headers</param>
    /// <returns>This request</returns>
    IRestRequest AddOrUpdateHeaders(ICollection<KeyValuePair<string, string>> headers);

    /// <summary>
    /// Shortcut to AddParameter(name, value, UrlSegment) overload
    /// </summary>
    /// <param name="name">Name of the segment to add</param>
    /// <param name="value">Value of the segment to add</param>
    /// <returns></returns>
    IRestRequest AddUrlSegment(string name, string value);

    /// <summary>
    /// Shortcut to AddParameter(name, value, UrlSegment) overload
    /// </summary>
    /// <param name="name">Name of the segment to add</param>
    /// <param name="value">Value of the segment to add</param>
    /// <param name="encode">Specify false if the value should not be encoded</param>
    /// <returns></returns>
    IRestRequest AddUrlSegment(string name, string value, bool encode);

    /// <summary>
    /// Shortcut to AddParameter(name, value, UrlSegment) overload
    /// </summary>
    /// <param name="name">Name of the segment to add</param>
    /// <param name="value">Value of the segment to add</param>
    /// <returns></returns>
    IRestRequest AddUrlSegment(string name, object value);

    /// <summary>
    /// Shortcut to AddParameter(name, value, QueryString) overload
    /// </summary>
    /// <param name="name">Name of the parameter to add</param>
    /// <param name="value">Value of the parameter to add</param>
    /// <returns></returns>
    IRestRequest AddQueryParameter(string name, string value);

    /// <summary>
    /// Shortcut to AddParameter(name, value, QueryString) overload
    /// </summary>
    /// <param name="name">Name of the parameter to add</param>
    /// <param name="value">Value of the parameter to add</param>
    /// <param name="encode">Whether parameter should be encoded or not</param>
    /// <returns></returns>
    IRestRequest AddQueryParameter(string name, string value, bool encode);

    void IncreaseNumAttempts();
}