;(function($){
    'use strict'

    function Calendar (options) {
            var that = this;

            this.trigger = options.trigger
            this.calendarSelector = '#calendar-7'
            this.times = {}

            if (options.allowTimeStart && options.allowTimeEnd) {
                this.times.allowHourStart = parseInt(options.allowTimeStart.split(':')[0], 10)
                this.times.allowHourEnd = parseInt(options.allowTimeEnd.split(':')[0], 10)

                this.times.allowMinuteStart = parseInt(options.allowTimeStart.split(':')[1], 10)
                this.times.allowMinuteEnd = parseInt(options.allowTimeEnd.split(':')[1], 10)
            }

            this.trigger.on('click', function (event) {
                event.stopPropagation()
                if ($(that.calendarSelector).length === 0) {
                    that.init()
                }
            });
            $(document).click(function (event) {
                if ($(event.target).parents('#calendar-7').length === 0) {
                    $(that.calendarSelector).remove()
                }
            });
        }
        Calendar.prototype.init = function () {
            // 未来七天
            var weeksOfzhTW = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']
            var today = new Date();
            this.year = today.getFullYear();
            this.month = Number(today.getMonth()) + 1;
            this.day = today.getDate();

            var html = '<div id="calendar-7" class="calendar-7">\
                            <div class="days">';

            for (var i = 0; i < 7; i++) {
                var classNames = i ===0 ? 'calendar-7-day active' : 'calendar-7-day';
                var month = today.getMonth()+1

                html += '<div class="'+classNames+'" data-year="'+today.getFullYear()+'" data-month="'+month+'" data-day="'+today.getDate()+'">\
                            <span>'+month+'/'+today.getDate()+'</span>\
                            <br/>\
                            <span>'+weeksOfzhTW[today.getDay()]+'</span>\
                        </div>';

                today.setDate(today.getDate() + 1);
            }
            
            html +=         '</div>\
                            <div class="hours"></div>\
                            <div class="minutes"></div>\
                        </div>';

            // 渲染日历
            $('body').append(html)
            var positionObj = this.trigger.get(0).getBoundingClientRect()

            // 给日历定位
            if ($(window).width() - positionObj.right < 10 ) {
                $('#calendar-7').css({
                    top: positionObj.top + this.trigger.outerHeight() + 5,
                    right: 0
                });
            } else {
                $('#calendar-7').css({
                    top: positionObj.top + this.trigger.outerHeight() + 5,
                    left: positionObj.left
                });
            }

            this.renderHours();
            this.dateClickHandler();
        }
        Calendar.prototype.getHours = function () {
            var today = new Date();
            var currentHour = today.getHours();
            var currentDay = today.getDate();

            var hours24 = [];
            for (var i = 0; i < 24; i++) {
                if (i > this.times.allowHourEnd || i < this.times.allowHourStart || (i < currentHour && this.day === currentDay)) {
                    hours24.push({
                        disabled: true,
                        hour: i + ':' + '00'
                    })
                } else {
                    hours24.push({
                        disabled: false,
                        hour: i + ':' + '00'
                    })
                }
            }
            return hours24
        }
        Calendar.prototype.renderHours = function () {
            var hours = this.getHours();
            var html = ''
            for (var i = 0; i < hours.length; i++) {
                if (hours[i].disabled) {
                    html += '<span class="calendar-7-hour disabled" data-hour="' + hours[i].hour + '">' + hours[i].hour + '</span>'
                } else {
                    html += '<span class="calendar-7-hour" data-hour="' + hours[i].hour + '">' + hours[i].hour + '</span>'
                }
            }
            $(this.calendarSelector).find('.hours').html(html).show().siblings('.minutes').hide()
        }
        Calendar.prototype.dateClickHandler = function () {
            var that = this

            // 綁定日期的點擊
            $('.calendar-7-day').click(function (event) {
                $('.calendar-7-day').removeClass('active')
                $(this).addClass('active')
                that.year = $(this).data('year')
                that.month = $(this).data('month')
                that.day = $(this).data('day')
                that.renderHours()
            });
            // 绑定小时的点击
            $(document).on('click', '.calendar-7-hour', function () {
                $('.calendar-7-hour').removeClass('active')
                $(this).addClass('active')
                that.hour = parseInt($(this).data('hour').split(':')[0], 10)
                if (!$(this).hasClass('disabled')) {
                    that.drawMinutes()
                }
            });
        }
        Calendar.prototype.drawMinutes = function () {
            var html = ''
            var today = new Date()
            var currentDay = today.getDate()
            var currentHour = today.getHours()
            var currentMinute = today.getMinutes()

            for (var i = 0, text = ''; i < 60;) {
                text = i < 10 ? '0' + i : i
                if ((currentHour === this.hour && currentMinute > i && currentDay === this.day) || (this.hour === this.times.allowHourEnd && this.times.allowMinuteEnd < i) || (this.hour === this.times.allowHourStart && this.times.allowMinuteStart > i)) {
                    html += '<span class="calendar-7-minute disabled" data-minute="' + text + '">' + this.hour + ':' + text + '</span>'
                } else {
                    html += '<span class="calendar-7-minute" data-minute="' + text + '">' + this.hour + ':' + text + '</span>'
                }
                i += 5;
            }

            $('#calendar-7 .hours').hide();

            $('#calendar-7 .minutes').html(html).show();

            this.minuteClickHandler();
        }
        Calendar.prototype.minuteClickHandler = function () {
            var that = this

            $('.calendar-7-minute').bind('click', function () {
                that.minute = $(this).data('minute');

                var time = that.year + '-' + that.month + '-' + that.day + ' ' + that.hour + ':' + that.minute

                if (!$(this).hasClass('disabled')) {
                    that.trigger.val(time);
                    $(that.calendarSelector).remove();
                }
                
                that.destroy();

            });
        }

        Calendar.prototype.destroy = function () {
            $('.calendar-7-minute').unbind()
        }

        $.fn.Calendar7 = function (options) {
            this.each(function (index, el) {
                var settings = {
                    trigger: $(this),
                    allowTimeStart: '',
                    allowTimeEnd: ''
                };
                new Calendar($.extend(true, settings, options));
            });
        }
})(jQuery)



