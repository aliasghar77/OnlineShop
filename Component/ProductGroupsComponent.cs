using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Repository;
using OnlineShop.Models;

namespace OnlineShop.Component
{
    public class ProductGroupsComponent:ViewComponent
    {
        private IGroupRepository _groupRepository;

        public ProductGroupsComponent(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return View("/Views/Component/ProductGroupsViewComponent.cshtml", _groupRepository.GetGroupForShow());
        }
    }
}
