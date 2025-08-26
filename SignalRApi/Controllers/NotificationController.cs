using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.NotificationDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult NotificationList()
        {
            var notifications = _notificationService.TGetListAll();
            return Ok(_mapper.Map<List<ResultNotificationDto>>(notifications));
        }
        [HttpGet("NotificationCountByStatusFalse")]
        public IActionResult NotificationCountByStatusFalse()
        {
            var notification = _notificationService.TNotificationCountByStatusFalse();
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }
        [HttpGet("GetAllNotificationByFalse")]
        public IActionResult GetAllNotificationByFalse()
        {
            var notifications = _notificationService.TGetAllNotificationByFalse();
            if (notifications == null || !notifications.Any())
            {
                return NotFound();
            }
            return Ok(notifications);
        }
        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            createNotificationDto.Status = false;
            createNotificationDto.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var value = _mapper.Map<Notification>(createNotificationDto);

            _notificationService.TAdd(value);
            return Ok("Ekleme işlemi başarıyla yapıldı.");

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var notification = _notificationService.TGetByID(id);
            if (notification == null)
            {
                return NotFound();
            }
            _notificationService.TDelete(notification);
            return Ok("Silme işlemi başarıyla yapıldı.");
        }
        [HttpGet("{id}")]
        public IActionResult GetNotification(int id)
        {
            var notification = _notificationService.TGetByID(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetByIdNotificationDto>(notification));
        }
        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            var value = _mapper.Map<Notification>(updateNotificationDto);


            _notificationService.TUpdate(value);
            return Ok("Güncelleme işlemi başarıyla yapıldı.");
        }
        [HttpGet("NotificationStatusChangeToFalse/{id}")]
        public IActionResult NotificationStatusChangeToFalse(int id)
        {
            _notificationService.TNotificationStatusChangeToFalse(id);
            return Ok("Güncelleme Yapıldı");


        }
        [HttpGet("NotificationStatusChangeToTrue/{id}")]
        public IActionResult NotificationStatusChangeToTrue(int id)
        {
            _notificationService.TNotificationStatusChangeToTrue(id);
            return Ok("Güncelleme Yapıldı");
        }
    }
}