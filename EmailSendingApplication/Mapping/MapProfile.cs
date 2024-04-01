using static NuGet.Packaging.PackagingConstants;
using System.Threading;
using AutoMapper;
using EmailSendingApplication.DTO_s;
using EmailSendingApplication.Models;

namespace EmailSendingApplication.Mapping
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<MailSenders, RequestMailSenderDTO>().ReverseMap();
            CreateMap<MailSenders, RespondMailSenderDTO>().ReverseMap();
            CreateMap<MailRecipient, RespondMailRecipientDTO>().ReverseMap();
            CreateMap<MailRecipient, RequestMailRecipientDTO>().ReverseMap();
            CreateMap<SentMail, RequestSentMailDTO>().ReverseMap();
            CreateMap<SentMail, RespondSentMailDTO>().ReverseMap();
            
        }
    }
}
