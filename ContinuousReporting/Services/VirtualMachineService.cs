using System.IO;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Management.Compute;
using Microsoft.WindowsAzure.Management.Compute.Models;
using System;
using System.Security.Cryptography.X509Certificates;

namespace ContinuousReporting.Services
{
    public class VirtualMachineService
    {
        private readonly string _serviceName;
        private readonly string _deploymentName;
        private readonly string _virtualMachineName;
        private readonly ComputeManagementClient _client;

        private VirtualMachineService()
        {
            _serviceName = System.Configuration.ConfigurationManager.AppSettings["serviceName"];
            _deploymentName = System.Configuration.ConfigurationManager.AppSettings["deploymentName"];
            _virtualMachineName = System.Configuration.ConfigurationManager.AppSettings["virtualMachineName"];

            var subscriptionId = System.Configuration.ConfigurationManager.AppSettings["subscriptionId"];
            var certificate = System.Configuration.ConfigurationManager.AppSettings["base64EncodedCertificate"];

            _client = new ComputeManagementClient(GetCredentials(subscriptionId, certificate));
        }

        private static VirtualMachineService _instance;
        public static VirtualMachineService Instance
        {
            get { return _instance ?? (_instance = new VirtualMachineService()); }
        }

        public void Start()
        {
            // var operationStatusResponse = _client.VirtualMachines.Start(_serviceName, _deploymentName, _virtualMachineName);
        }

        public void Shutdown()
        {
            // var operationStatusResponse = _client.VirtualMachines.Shutdown(_serviceName, _deploymentName, _virtualMachineName, new VirtualMachineShutdownParameters());
        }

        private static SubscriptionCloudCredentials GetCredentials(string subscriptionId, string base64EncodedCertificate)
        {
            return new CertificateCloudCredentials(subscriptionId, new X509Certificate2(Convert.FromBase64String(base64EncodedCertificate), "321654987"));
        }
    }
}