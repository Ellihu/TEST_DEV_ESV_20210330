using Datos.UnitOfWork;
using Entidades.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Servicios
{
    public class ApiServiceCustomers : IApiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private string EndPointAuth;
        private string EndPointReport;
        private string access_token;
        private WebClient wc;

        public ApiServiceCustomers(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            EndPointAuth = _configuration["ApiReports:EndPointAuth"];
            EndPointReport = _configuration["ApiReports:EndPointReport"];
            wc = new WebClient();
        }

        public void Autenticar()
        {
            string endpoint = EndPointAuth;
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                Username = _configuration["ApiReports:Username"],
                Password = _configuration["ApiReports:Password"],
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                access_token = JsonConvert.DeserializeObject<dynamic>(response).Data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsAutenticate()
        {
            return !string.IsNullOrEmpty(access_token);
        }

        public dynamic Listar()
        {
            string endpoint = EndPointReport;
            WebClient wc = GetWebClient();

            try
            {
                string jsonResponse = wc.DownloadString(endpoint);
                CustomersVModelResponse responseModel = JsonConvert.DeserializeObject<CustomersVModelResponse>(jsonResponse);


                return responseModel.Data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        WebClient GetWebClient()
        {
            wc.Encoding = System.Text.Encoding.UTF8;
            wc.Headers["Content-Type"] = "application/json; charset=utf-8";
            wc.Headers["Authorization"] = access_token;
            return wc;
        }
    }
}
