using Microsoft.Extensions.Options;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Reflection.Metadata;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace GrpcServiceApp.Services
{
    public class HHResumeService
    {
        public ResumeListResponse GetResumes(string name)
        {
            using var httpClient = new HttpClient();
            ResumeListResponse resumes = new ResumeListResponse();

            httpClient.DefaultRequestHeaders.Add("User-Agent", "HH-User-Agent");
            var url = $"https://api.hh.ru/vacancies?per_page=100&title={name}";

            using var response = httpClient.GetStringAsync(url);
            var json = JObject.Parse(response.Result);

            foreach (var item in json["items"])
            {
                var resume = new SingleResumeResponse();
                string? title = item["name"]?.ToString();
                string? description = item["snippet"]?["responsibility"]?.ToString() + " " + item["snippet"]?["requirement"]?.ToString();
                string? link = item["alternate_url"]?.ToString();
                string? location = item["area"]?["name"]?.ToString();
                string? salary = item["salary"] != null && item["salary"]?.ToString() != string.Empty ? $"{item["salary"]?["from"]} - {item["salary"]?["to"]} {item["salary"]?["currency"]}" : "Не указана";
                string? experience = item["experience"]?["name"]?.ToString();
                string? company = item["employer"]?["name"]?.ToString();

                resume.Title = title;
                resume.Description = description;
                resume.Link = link;
                resume.Location = location;
                resume.Salary = salary;
                resume.Experience = experience;
                resume.Company = company;

                resumes.List.Add(resume);
            }

            return resumes;

        }
    }
}
