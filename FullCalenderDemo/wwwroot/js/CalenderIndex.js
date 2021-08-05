var myCalendar;
function calculateEventStartDate(date, startTime) {
    return `${formatDate(date)}T${formatTime(startTime)}`
}
function calculateEventEndDate(date, endTime) {
    return `${formatDate(date)}T${formatTime(endTime)}`
}
function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}
function formatTime(date) {
    var d = new Date(date),
        hour = '' + (d.getHours() + 1),
        min = '' + d.getMinutes(),
        sec = "00";

    return [hour, min, sec].join(':');
}
document.addEventListener('DOMContentLoaded', intiateCalendar);

function intiateCalendar() {
    var calendarEl = document.getElementById('calendar');
    myCalendar = new FullCalendar.Calendar(calendarEl, {

        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
        },
        navLinks: true, // can click day/week names to navigate views
        editable: false,
        selectable: true,
        themeSystem: 'bootstrap',
        dayMaxEventRows: true, // for all non-TimeGrid views
        views: {
            timeGrid: {
                dayMaxEventRows: 2 // adjust to 6 only for timeGridWeek/timeGridDay
            }
        },
        eventDidMount: function (info) {
                let tooltip = formatEventTooltip(info.event)
            $(info.el).tooltip({
                template: `${tooltip.template}`,
                title: `${tooltip.tooltipText}`,
                placement: "top",
                trigger: " hover",
                container: "body",
                
                
            });
            setTimeout(function () {

            if (info.event.extendedProps.eventType == 0)
                $($(info.el).parent()).addClass("internalEvent")
            else
                $($(info.el).parent()).addClass("externalEvent")
            },0);

        },
     
        eventSources: [
            {
                events: function (info, successCallback, failureCallback) {
                    $.ajax({
                        url: `${location.href}/GetAllEvents?ReturnList=true`,
                        dataType:"json",
                        type: 'GET',
                        success: function (response) {
                            var events = [];
                            $(response).each(function () {
                                events.push({
                                    id: $(this).attr('id'),
                                    title: $(this).attr('title'),
                                    //start: $(this).attr('startTime'),
                                    start: calculateEventStartDate($(this).attr('date'), $(this).attr('startTime')),
                                    //end: $(this).attr('endTime'),
                                    end:calculateEventEndDate($(this).attr('date'), $(this).attr('endTime')),

                                    //backgroundColor: "blue",
                                    //eventColor: "blue",
                                   // color: "blue",
                                   // textColor:"green",
                                    extendedProps: {
                                        description: $(this).attr('description'),
                                        createdBy: $(this).attr('createdBy'),
                                        eventType: $(this).attr('eventType'),
                                    },
                                    className: getEventClassByEventType($(this).attr('eventType')),
                                   // allDay: $(this).attr('allDay'),
                                });
                            });
                                successCallback(events);
                        }
                    });
                },
               
              
            }
        ]

    });
    myCalendar.render();
}
function getEventClassByEventType(eventType) {
    if (eventType == 0)
        return "internalEvent";
    else
        return "externalEvent";
}
function formatEventTooltip(eventObj) {
    let tooltipText = "";
    let eventType = {
        0: "Internal Event",
        1:"External Event"
    }
    let tooltipObj = {
        Title: eventObj.title,
        Start: eventObj.start.toLocaleTimeString(),
        End: eventObj.end.toLocaleTimeString(),
        "Created By": eventObj.extendedProps.createdBy,
        "Event Type": eventType[eventObj.extendedProps.eventType],
       // "Edit Event": `<a href="${location.href}Evenet/Edit/${eventObj.id}">Edit</a>`
    };
    for (const property in tooltipObj) {
        tooltipText += `${property} : ${tooltipObj[property]} \n`;
    }

    //let template = `<div class="tooltip tooltip-custom" style="width:fit-content">
    //                    <div class="">Details</div>
    //                    <div class="tooltip-arrow"></div>
    //                    <pre class="tooltip-inner" style="white-space: pre-line;"></pre>
    //                </div>`
    let template = `<div class="fc-popover popover fc-more-popover fc-day fc-day-sun fc-day-today zindex2000 " style="top: 31px; left: 1px;">
                        <div class="fc-popover-header popover-header">
                            <span class="fc-popover-title">Details</span>
                        </div>
                        <div class="tooltip-arrow"></div>
                        <div class="fc-popover-body popover-body">
                            <div class="fc-daygrid-event-harness ">
                                <div class="tooltip-inner" style="white-space: pre-line;"></div>
                            </div>

                        </div>
                    </div>`

   

    //return tooltipText;
    return { template, tooltipText };

}

