using Grpc.Core;
using GrpcServiceApp;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace GrpcServiceApp.Services
{
    public class ResumeServiceImpl : GrpcServiceApp.ResumeService.ResumeServiceBase
    {
        public override async Task getResumeList(IAsyncStreamReader<ResumeRequest> requestStream, IServerStreamWriter<ResumeListResponse> responseStream, ServerCallContext context)
        {
            string name = requestStream.Current.Title;
            string sort = requestStream.Current.Sorting;

            ResumeListResponse resumes;
            HHResumeService service = new HHResumeService();

            resumes = service.GetResumes(sort);

            await responseStream.WriteAsync(resumes);
            
        }
    }
}
