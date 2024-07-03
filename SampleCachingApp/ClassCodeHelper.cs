using SampleCachingApp.Services;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace SampleCachingApp
{
    public static class EmployeeServiceHelper
    {
        public static string GenerateILByteCodeString(string methodName)
        {
            Type myClassType = typeof(EmployeeService);
            MethodInfo methodInfo = myClassType.GetMethod(methodName);

            if (methodInfo == null)
                return string.Empty;

            byte[] ilBytes = methodInfo.GetMethodBody()?.GetILAsByteArray();

            if (ilBytes == null)
                return string.Empty;
            return Convert.ToBase64String(ilBytes);
        }
    }
}
