using System.Collections.Generic;
using System.Configuration;
using GoodDataApi.Payload;
using GoodDataApi.Payload.Filters;

namespace GoodDataApi.Resources
{
    public interface IMandatoryUserFilter
    {
        GoodDataResponse<FilterQueryContainer> All(string projectId);
        GoodDataResponse<FilterCreateResponse> Create(string projectId, FilterCreateRequest request);
        GoodDataResponse<FilterAssignmentResponse> Assign(string projectId, FilterAssignmentRequest request);
        GoodDataResponse<FilterCreateResponse> Update(string uri, FilterCreateRequest request);
        GoodDataResponse<object> Delete(string uri);
        GoodDataResponse<UserFilterGetResponse> Get(string uri);
        GoodDataResponse<GetFilterAssignmentsResponse> GetAssignments(string projectId, string profileId);
    }

    internal sealed class MandatoryUserFilter : IMandatoryUserFilter
    {
        private readonly IInternalGoodDataConnection _connection;

        public MandatoryUserFilter(IInternalGoodDataConnection connection)
        {
            _connection = connection;
        }

        public GoodDataResponse<FilterQueryContainer> All(string projectId)
        {
            var uri = Urls.All(projectId);
            return _connection.Get<FilterQueryContainer>(uri);
        }

        public GoodDataResponse<FilterCreateResponse> Create(string projectId, FilterCreateRequest request)
        {
            return _connection.Post<FilterCreateResponse>(Urls.Create(projectId), request);
        }

        public GoodDataResponse<FilterAssignmentResponse> Assign(string projectId, FilterAssignmentRequest request)
        {
            return _connection.Post<FilterAssignmentResponse>(Urls.Assign(projectId), request);
        }

        public GoodDataResponse<FilterCreateResponse> Update(string uri, FilterCreateRequest request)
        {
            return _connection.Post<FilterCreateResponse>(uri, request);
        }

        public GoodDataResponse<object> Delete(string uri)
        {
            return _connection.Delete<object>(uri);
        }

        public GoodDataResponse<UserFilterGetResponse> Get(string uri)
        {
            return _connection.Get<UserFilterGetResponse>(uri);
        }

        public GoodDataResponse<GetFilterAssignmentsResponse> GetAssignments(string projectId, string profileId)
        {
            return _connection.Get<GetFilterAssignmentsResponse>(Urls.GetAssignments(projectId, profileId));
        }

        private static class Urls
        {
            public static string All(string projectId)
            {
                return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.MandatoryUserFilter.All", "/gdc/md/{0}/query/userfilters"), projectId);
            }

            public static string Create(string projectId)
            {
                return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.MandatoryUserFilter.Create", "/gdc/md/{0}/obj"), projectId);
            }

            public static string Assign(string projectId)
            {
                return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.MandatoryUserFilter.Assign", "/gdc/md/{0}/userfilters"), projectId);
            }

            public static string GetAssignments(string projectId, string profileId)
            {
                return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.MandatoryuserFilter.GetAssignments", "/gdc/md/{0}/userfilters?users={1}"), projectId, profileId);
            }


        }
    }
}