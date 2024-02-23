using AutoMapper;
using Notes.Api.Entities;

namespace Notes.API.Profiles
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            // map from Note (entity) to Note, and back
            CreateMap<Note, Model.Note>().ReverseMap();

            // map from ImageForCreation to Image
            CreateMap<Model.NoteForCreation, Note>()
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.OwnerId, options => options.Ignore());

            // map from NoteForUpdate to Note
            CreateMap<Model.NoteForUpdate, Note>()
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.OwnerId, options => options.Ignore());
        }
    }
}
