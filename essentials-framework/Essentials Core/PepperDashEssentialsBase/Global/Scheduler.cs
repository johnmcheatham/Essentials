﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.Scheduler;

using PepperDash.Core;

namespace PepperDash.Essentials.Core
{
    /// <summary>
    /// Global Scheduler for the system
    /// </summary>
    public static class Scheduler
    {
        private static Dictionary<string, ScheduledEventGroup> EventGroups = new Dictionary<string,ScheduledEventGroup>();

        static Scheduler()
        {
            CrestronConsole.AddNewConsoleCommand(ClearEventsFromGroup, "ClearAllEvents", "Clears all scheduled events for this group", ConsoleAccessLevelEnum.AccessOperator);

            CrestronConsole.AddNewConsoleCommand(ListAllEventGroups, "ListAllEventGroups", "Lists all the event groups by key", ConsoleAccessLevelEnum.AccessOperator);
        }

        /// <summary>
        /// Clears (deletes) all events from a group
        /// </summary>
        /// <param name="groupName"></param>
        static void ClearEventsFromGroup(string groupName)
        {
            var group = EventGroups[groupName];

            if (group != null)
                group.ClearAllEvents();
            else
                Debug.Console(0, "[Scheduler]: Unable to delete events from group '{0}'.  Group not found in EventGroups dictionary.", groupName);
        }

        static void ListAllEventGroups(string command)
        {
            Debug.Console(0, "Event Groups:");
            foreach (var group in EventGroups)
            {
                Debug.Console(0, "{0}", group.Key);
            }
        }

        /// <summary>
        /// Adds the event group to the global list
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static void AddEventGroup(ScheduledEventGroup eventGroup)
        {
            // Add this group to the global collection
            if (!EventGroups.ContainsKey(eventGroup.Name))
                EventGroups.Add(eventGroup.Name, eventGroup);
        }

        /// <summary>
        /// Removes the event group from the global list
        /// </summary>
        /// <param name="eventGroup"></param>
        public static void RemoveEventGroup(ScheduledEventGroup eventGroup)
        {
            if(!EventGroups.ContainsKey(eventGroup.Name))
                EventGroups.Remove(eventGroup.Name);
        }
    }

    public static class SchedulerUtilities
    {
        /// <summary>
        /// Checks the day of week in eventTime to see if it matches the weekdays defined in the recurrence enum.
        /// </summary>
        /// <param name="eventTime"></param>
        /// <param name="recurrence"></param>
        /// <returns></returns>
        public static bool CheckIfDayOfWeekMatchesRecurrenceDays(DateTime eventTime, ScheduledEventCommon.eWeekDays recurrence)
        {
            bool isMatch = false;

            var dayOfWeek = eventTime.DayOfWeek;

            Debug.Console(1, "[Scheduler]: eventTime day of week is: {0}", dayOfWeek);

            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    {
                        if ((recurrence & ScheduledEventCommon.eWeekDays.Sunday) == ScheduledEventCommon.eWeekDays.Sunday)
                            isMatch = true;
                        break;
                    }
                case DayOfWeek.Monday:
                    {
                        if ((recurrence & ScheduledEventCommon.eWeekDays.Monday) == ScheduledEventCommon.eWeekDays.Monday)
                            isMatch = true;
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        if ((recurrence & ScheduledEventCommon.eWeekDays.Tuesday) == ScheduledEventCommon.eWeekDays.Tuesday)
                            isMatch = true;
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        if ((recurrence & ScheduledEventCommon.eWeekDays.Wednesday) == ScheduledEventCommon.eWeekDays.Wednesday)
                            isMatch = true;
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        if ((recurrence & ScheduledEventCommon.eWeekDays.Thursday) == ScheduledEventCommon.eWeekDays.Thursday)
                            isMatch = true;
                        break;
                    }
                case DayOfWeek.Friday:
                    {
                        if ((recurrence & ScheduledEventCommon.eWeekDays.Friday) == ScheduledEventCommon.eWeekDays.Friday)
                            isMatch = true;
                        break;
                    }
                case DayOfWeek.Saturday:
                    {
                        if ((recurrence & ScheduledEventCommon.eWeekDays.Saturday) == ScheduledEventCommon.eWeekDays.Saturday)
                            isMatch = true;
                        break;
                    }
            }

            Debug.Console(1, "[Scheduler]: eventTime day of week matches recurrence days: {0}", isMatch);

            return isMatch;
        }
    }
}