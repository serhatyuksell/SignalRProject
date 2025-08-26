using AutoMapper;
using SignalR.DtoLayer.MenuTableDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Mapping
{
    public class MenuTableMapping:Profile
    {
        public MenuTableMapping()
        {
            CreateMap<MenuTable, ResultMenuTableDto>().ReverseMap();
            CreateMap<CreateMenuTableDto, MenuTable>().ReverseMap();
            CreateMap<UpdateMenuTableDto,MenuTable>().ReverseMap();
            CreateMap<GetMenuTableDto,MenuTable>().ReverseMap();
        }
    }
}
