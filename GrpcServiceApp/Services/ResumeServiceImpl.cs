using Grpc.Core;

namespace GrpcServiceApp.Services
{
    public class ResumeServiceImpl : ResumeService.ResumeServiceBase
    {
        public override async Task getResumeList(IAsyncStreamReader<ResumeRequest> requestStream, IServerStreamWriter<ResumeListResponse> responseStream, ServerCallContext context)
        {
            string name = requestStream.Current.Title;
            string sort = requestStream.Current.Sorting;
            ResumeListResponse resumes;
            HHResumeService service = new HHResumeService();
            try
            {
                resumes = service.GetResumes(name);
                //if(sort == "ASC")
                //    resumes.List.Order();
                //else if(sort == "DESC")
                //    resumes.List.OrderDescending(); 
                await responseStream.WriteAsync(resumes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await responseStream.WriteAsync(new ResumeListResponse());
            }
            
        }
    }
}
