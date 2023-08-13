using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.EntidadesDto;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacion.Mapa
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<UsuarioApp, UsuarioDto>().ReverseMap();
            CreateMap<UsuarioDto, UsuarioApp>().ReverseMap();

            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<MenuDto, Menu>().ReverseMap();
            CreateMap<Menu, SubMenuDto>().ReverseMap();
            CreateMap<SubMenuDto, Menu>().ReverseMap();

            CreateMap<RolesMenu, RolesMenuDto>().ReverseMap();
            CreateMap<RolesMenuDto, RolesMenu>().ReverseMap();
        }
    }
}
