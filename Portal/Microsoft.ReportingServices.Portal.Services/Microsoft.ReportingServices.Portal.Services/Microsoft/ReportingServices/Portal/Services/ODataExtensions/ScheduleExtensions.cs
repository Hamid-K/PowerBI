using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x0200004A RID: 74
	internal static class ScheduleExtensions
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0001125C File Offset: 0x0000F45C
		public static global::Model.Schedule ToWebAPI(this Microsoft.ReportingServices.Library.Soap.Schedule soapSchedule, int utcOffsetInMinutes = 0)
		{
			if (soapSchedule == null)
			{
				throw new ArgumentNullException("soapSchedule");
			}
			return new global::Model.Schedule
			{
				Id = new Guid(soapSchedule.ScheduleID),
				Name = soapSchedule.Name,
				Definition = soapSchedule.Definition.ToWebAPI(),
				Description = soapSchedule.GetDescription(utcOffsetInMinutes),
				Creator = soapSchedule.Creator,
				NextRunTime = ((soapSchedule.NextRunTime.Ticks == 0L) ? default(DateTimeOffset) : soapSchedule.NextRunTime),
				NextRunTimeSpecified = soapSchedule.NextRunTimeSpecified,
				LastRunTime = ((soapSchedule.LastRunTime.Ticks == 0L) ? default(DateTimeOffset) : soapSchedule.LastRunTime),
				LastRunTimeSpecified = soapSchedule.LastRunTimeSpecified,
				ReferencesPresent = soapSchedule.ReferencesPresent,
				State = (global::Model.ScheduleStateEnum)Enum.Parse(typeof(global::Model.ScheduleStateEnum), soapSchedule.State.ToString())
			};
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00011370 File Offset: 0x0000F570
		public static Microsoft.ReportingServices.Library.Soap.Schedule ToSoapAPI(this global::Model.Schedule webApiSchedule)
		{
			if (webApiSchedule == null)
			{
				throw new ArgumentNullException("webApiSchedule");
			}
			return new Microsoft.ReportingServices.Library.Soap.Schedule
			{
				ScheduleID = webApiSchedule.Id.ToString(),
				Name = webApiSchedule.Name,
				Definition = webApiSchedule.Definition.ToSoapAPI(),
				Description = webApiSchedule.Description,
				Creator = webApiSchedule.Creator,
				NextRunTime = webApiSchedule.NextRunTime.LocalDateTime,
				NextRunTimeSpecified = webApiSchedule.NextRunTimeSpecified,
				LastRunTime = webApiSchedule.LastRunTime.LocalDateTime,
				LastRunTimeSpecified = webApiSchedule.LastRunTimeSpecified,
				ReferencesPresent = webApiSchedule.ReferencesPresent,
				State = (Microsoft.ReportingServices.Library.Soap.ScheduleStateEnum)Enum.Parse(typeof(Microsoft.ReportingServices.Library.Soap.ScheduleStateEnum), webApiSchedule.State.ToString())
			};
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0001145C File Offset: 0x0000F65C
		public static global::Model.ScheduleDefinition ToWebAPI(this Microsoft.ReportingServices.Library.Soap.ScheduleDefinition soapDefinition)
		{
			global::Model.ScheduleDefinition scheduleDefinition = new global::Model.ScheduleDefinition
			{
				StartDateTime = ((soapDefinition.StartDateTime.Ticks == 0L) ? default(DateTimeOffset) : soapDefinition.StartDateTime),
				EndDate = ((soapDefinition.EndDate.Ticks == 0L) ? default(DateTimeOffset) : soapDefinition.EndDate),
				EndDateSpecified = soapDefinition.EndDateSpecified
			};
			if (soapDefinition.Item != null)
			{
				Microsoft.ReportingServices.Library.Soap.MonthlyRecurrence monthlyRecurrence = soapDefinition.Item as Microsoft.ReportingServices.Library.Soap.MonthlyRecurrence;
				if (monthlyRecurrence != null)
				{
					CultureInfo clientPrimaryCulture = Localization.ClientPrimaryCulture;
					if (clientPrimaryCulture.TextInfo.ListSeparator != ScheduleExtensions.InvariantListSeparator)
					{
						monthlyRecurrence.Days = monthlyRecurrence.Days.Replace(clientPrimaryCulture.TextInfo.ListSeparator, ScheduleExtensions.InvariantListSeparator);
					}
				}
				Type targetType = ScheduleExtensions.OccurenceTypeMap[soapDefinition.Item.GetType()];
				object obj = soapDefinition.Item.ConvertTo(targetType);
				scheduleDefinition.Recurrence.GetType().GetProperties().First((PropertyInfo p) => p.PropertyType == targetType)
					.SetValue(scheduleDefinition.Recurrence, obj);
			}
			return scheduleDefinition;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00011598 File Offset: 0x0000F798
		public static Microsoft.ReportingServices.Library.Soap.ScheduleDefinition ToSoapAPI(this global::Model.ScheduleDefinition webApiSchedule)
		{
			Microsoft.ReportingServices.Library.Soap.ScheduleDefinition scheduleDefinition = new Microsoft.ReportingServices.Library.Soap.ScheduleDefinition
			{
				StartDateTime = webApiSchedule.StartDateTime.LocalDateTime,
				EndDate = webApiSchedule.EndDate.LocalDateTime,
				EndDateSpecified = webApiSchedule.EndDateSpecified
			};
			List<object> list = (from p in webApiSchedule.Recurrence.GetType().GetProperties()
				where p.GetValue(webApiSchedule.Recurrence) != null
				select p.GetValue(webApiSchedule.Recurrence)).ToList<object>();
			if (list.Count<object>() > 1)
			{
				throw new PortalException(SR.Error_InvalidScheduleRecurrenceObject, HttpStatusCode.BadRequest, Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.InvalidContent);
			}
			if (list.Any<object>())
			{
				object obj = list.First<object>();
				Type recurrenceType = list.FirstOrDefault<object>().GetType();
				Type key = ScheduleExtensions.OccurenceTypeMap.FirstOrDefault((KeyValuePair<Type, Type> p) => p.Value == recurrenceType).Key;
				scheduleDefinition.Item = (Microsoft.ReportingServices.Library.Soap.RecurrencePattern)obj.ConvertTo(key);
				Microsoft.ReportingServices.Library.Soap.MonthlyRecurrence monthlyRecurrence = scheduleDefinition.Item as Microsoft.ReportingServices.Library.Soap.MonthlyRecurrence;
				if (monthlyRecurrence != null)
				{
					CultureInfo clientPrimaryCulture = Localization.ClientPrimaryCulture;
					if (clientPrimaryCulture.TextInfo.ListSeparator != ScheduleExtensions.InvariantListSeparator)
					{
						monthlyRecurrence.Days = monthlyRecurrence.Days.Replace(ScheduleExtensions.InvariantListSeparator, clientPrimaryCulture.TextInfo.ListSeparator);
					}
				}
				Microsoft.ReportingServices.Library.Soap.MinuteRecurrence minuteRecurrence = scheduleDefinition.Item as Microsoft.ReportingServices.Library.Soap.MinuteRecurrence;
				if (minuteRecurrence != null && minuteRecurrence.MinutesInterval < 1)
				{
					throw new PortalException(SR.Error_InvalidScheduleRecurrenceObject, HttpStatusCode.BadRequest, Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.InvalidContent);
				}
			}
			return scheduleDefinition;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00011738 File Offset: 0x0000F938
		public static Microsoft.SqlServer.ReportingServices2010.ScheduleDefinition ToSoapProxyAPI(this global::Model.ScheduleDefinition webApiSchedule)
		{
			List<object> list = (from p in webApiSchedule.Recurrence.GetType().GetProperties()
				where p.GetValue(webApiSchedule.Recurrence) != null
				select p.GetValue(webApiSchedule.Recurrence)).ToList<object>();
			if (list.Count<object>() > 1)
			{
				throw new PortalException(SR.Error_InvalidScheduleRecurrenceObject, HttpStatusCode.BadRequest, Microsoft.ReportingServices.Portal.Interfaces.Enums.ErrorCode.InvalidContent);
			}
			Microsoft.SqlServer.ReportingServices2010.ScheduleDefinition scheduleDefinition = new Microsoft.SqlServer.ReportingServices2010.ScheduleDefinition
			{
				StartDateTime = webApiSchedule.StartDateTime.DateTime,
				EndDate = webApiSchedule.EndDate.DateTime,
				EndDateSpecified = webApiSchedule.EndDateSpecified
			};
			if (list.Any<object>())
			{
				object obj = list.FirstOrDefault<object>();
				Type recurrenceType = list.FirstOrDefault<object>().GetType();
				Type key = ScheduleExtensions.OccurenceTypeMap.FirstOrDefault((KeyValuePair<Type, Type> p) => p.Value == recurrenceType).Key;
				scheduleDefinition.Item = (Microsoft.SqlServer.ReportingServices2010.RecurrencePattern)obj.ConvertTo(key);
			}
			return scheduleDefinition;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00011854 File Offset: 0x0000FA54
		public static string GetDescription(this Microsoft.ReportingServices.Library.Soap.ScheduleDefinition soapScheduleDefinition, int utcOffsetInMinutes)
		{
			string text;
			try
			{
				Microsoft.ReportingServices.Diagnostics.Task task = new Microsoft.ReportingServices.Diagnostics.Task(Guid.NewGuid());
				soapScheduleDefinition.PopulateTaskWithThis(task);
				text = task.Trigger.ComputeDescription(utcOffsetInMinutes);
			}
			catch (ReportCatalogException)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001189C File Offset: 0x0000FA9C
		public static string GetDescription(this Microsoft.ReportingServices.Library.Soap.Schedule soapSchedule, int utcOffsetInMinutes)
		{
			string description = soapSchedule.Definition.GetDescription(utcOffsetInMinutes);
			if (!string.IsNullOrEmpty(description))
			{
				return description;
			}
			return soapSchedule.Description;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public static string ToMatchData(this global::Model.ScheduleDefinition scheduleDefinition)
		{
			string empty = string.Empty;
			return Microsoft.ReportingServices.Library.Soap.ScheduleDefinitionOrReference.ThisToXml(scheduleDefinition.ToSoapAPI(), out empty);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000118E8 File Offset: 0x0000FAE8
		public static object ConvertTo(this object source, Type type)
		{
			object obj = Activator.CreateInstance(type);
			foreach (PropertyInfo propertyInfo in source.GetType().GetProperties())
			{
				PropertyInfo property = obj.GetType().GetProperty(propertyInfo.Name);
				if (property != null)
				{
					if (ScheduleExtensions.IsSystemType(property.PropertyType))
					{
						property.SetValue(obj, propertyInfo.GetValue(source, null));
					}
					else
					{
						property.SetValue(obj, propertyInfo.GetValue(source, null).ConvertTo(property.PropertyType));
					}
				}
			}
			return obj;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00011974 File Offset: 0x0000FB74
		private static bool IsSystemType(Type type)
		{
			return !type.IsClass || type == typeof(string) || type == typeof(DateTime);
		}

		// Token: 0x040000BC RID: 188
		private static Dictionary<Type, Type> OccurenceTypeMap = new Dictionary<Type, Type>
		{
			{
				typeof(Microsoft.ReportingServices.Library.Soap.MinuteRecurrence),
				typeof(global::Model.MinuteRecurrence)
			},
			{
				typeof(Microsoft.ReportingServices.Library.Soap.DailyRecurrence),
				typeof(global::Model.DailyRecurrence)
			},
			{
				typeof(Microsoft.ReportingServices.Library.Soap.WeeklyRecurrence),
				typeof(global::Model.WeeklyRecurrence)
			},
			{
				typeof(Microsoft.ReportingServices.Library.Soap.MonthlyRecurrence),
				typeof(global::Model.MonthlyRecurrence)
			},
			{
				typeof(Microsoft.ReportingServices.Library.Soap.MonthlyDOWRecurrence),
				typeof(global::Model.MonthlyDOWRecurrence)
			}
		};

		// Token: 0x040000BD RID: 189
		private static Dictionary<Type, Type> RSSoapProxyOccurenceTypeMap = new Dictionary<Type, Type>
		{
			{
				typeof(Microsoft.SqlServer.ReportingServices2010.MinuteRecurrence),
				typeof(global::Model.MinuteRecurrence)
			},
			{
				typeof(Microsoft.SqlServer.ReportingServices2010.DailyRecurrence),
				typeof(global::Model.DailyRecurrence)
			},
			{
				typeof(Microsoft.SqlServer.ReportingServices2010.WeeklyRecurrence),
				typeof(global::Model.WeeklyRecurrence)
			},
			{
				typeof(Microsoft.SqlServer.ReportingServices2010.MonthlyRecurrence),
				typeof(global::Model.MonthlyRecurrence)
			},
			{
				typeof(Microsoft.SqlServer.ReportingServices2010.MonthlyDOWRecurrence),
				typeof(global::Model.MonthlyDOWRecurrence)
			}
		};

		// Token: 0x040000BE RID: 190
		private static string InvariantListSeparator = ",";
	}
}
