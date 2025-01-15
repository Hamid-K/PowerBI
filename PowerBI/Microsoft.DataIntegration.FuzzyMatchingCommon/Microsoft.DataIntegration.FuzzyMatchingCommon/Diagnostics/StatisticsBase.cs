using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class StatisticsBase : IStatistics
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0001357A File Offset: 0x0001177A
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00013582 File Offset: 0x00011782
		public bool EnableTimers { get; set; }

		// Token: 0x0600025A RID: 602 RVA: 0x0001358C File Offset: 0x0001178C
		public StatisticsBase()
		{
			foreach (FieldInfo fieldInfo in base.GetType().GetFields())
			{
				if (fieldInfo.FieldType != typeof(Stopwatch))
				{
					this.m_fields.Add(fieldInfo);
				}
			}
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties(16))
			{
				if (propertyInfo.PropertyType != typeof(Stopwatch) && propertyInfo.GetIndexParameters().Length == 0 && !propertyInfo.Name.Equals("Properties") && !propertyInfo.Name.Equals("EnableTimers"))
				{
					this.m_fields.Add(propertyInfo);
				}
			}
			foreach (FieldInfo fieldInfo2 in base.GetType().GetFields())
			{
				if (fieldInfo2.FieldType == typeof(Stopwatch))
				{
					this.m_fields.Add(fieldInfo2);
				}
			}
			foreach (PropertyInfo propertyInfo2 in base.GetType().GetProperties())
			{
				if (propertyInfo2.PropertyType == typeof(Stopwatch) && propertyInfo2.GetIndexParameters().Length == 0 && !propertyInfo2.Name.Equals("Properties") && !propertyInfo2.Name.Equals("EnableTimers"))
				{
					this.m_fields.Add(propertyInfo2);
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00013705 File Offset: 0x00011905
		public virtual IEnumerable<string> Properties
		{
			get
			{
				foreach (object obj in this.m_fields)
				{
					if (obj is FieldInfo)
					{
						FieldInfo fieldInfo = (FieldInfo)obj;
						yield return fieldInfo.Name;
					}
					else if (obj is PropertyInfo)
					{
						PropertyInfo propertyInfo = (PropertyInfo)obj;
						yield return propertyInfo.Name;
					}
				}
				List<object>.Enumerator enumerator = default(List<object>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x1700005E RID: 94
		public virtual double this[string propertyName]
		{
			get
			{
				foreach (object obj in this.m_fields)
				{
					if (obj is FieldInfo)
					{
						FieldInfo fieldInfo = (FieldInfo)obj;
						if (fieldInfo.Name.Equals(propertyName))
						{
							if (typeof(Stopwatch) == fieldInfo.FieldType)
							{
								return ((Stopwatch)fieldInfo.GetValue(this)).Elapsed.TotalMilliseconds;
							}
							if (typeof(long) == fieldInfo.FieldType)
							{
								return (double)((long)fieldInfo.GetValue(this));
							}
							return (double)fieldInfo.GetValue(this);
						}
					}
					else if (obj is PropertyInfo)
					{
						PropertyInfo propertyInfo = (PropertyInfo)obj;
						if (propertyInfo.Name.Equals(propertyName))
						{
							if (typeof(Stopwatch) == propertyInfo.PropertyType)
							{
								return ((Stopwatch)propertyInfo.GetValue(this, null)).Elapsed.TotalMilliseconds;
							}
							if (typeof(long) == propertyInfo.PropertyType)
							{
								return (double)((long)propertyInfo.GetValue(this, null));
							}
							return (double)propertyInfo.GetValue(this, null);
						}
					}
				}
				throw new ArgumentException(string.Format("Property {0} not found.", propertyName));
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000138A8 File Offset: 0x00011AA8
		public void Start(Stopwatch stopwatch)
		{
			if (this.EnableTimers)
			{
				stopwatch.Start();
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000138B8 File Offset: 0x00011AB8
		public void Stop(Stopwatch stopwatch)
		{
			if (this.EnableTimers)
			{
				stopwatch.Stop();
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000138C8 File Offset: 0x00011AC8
		public virtual string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.m_fields)
			{
				if (obj is FieldInfo)
				{
					FieldInfo fieldInfo = (FieldInfo)obj;
					object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(StatisticAttribute), false);
					if (customAttributes == null || customAttributes.Length == 0 || !(customAttributes[0] as StatisticAttribute).Ignore)
					{
						if (typeof(Stopwatch) == fieldInfo.FieldType)
						{
							if (this.EnableTimers)
							{
								stringBuilder.AppendLine(string.Format(CultureInfo.CurrentUICulture, "{0}: {1:F2} ms", new object[]
								{
									fieldInfo.Name,
									((Stopwatch)fieldInfo.GetValue(this)).Elapsed.TotalMilliseconds
								}));
							}
						}
						else if (typeof(IStatistics) == fieldInfo.FieldType)
						{
							if ((IStatistics)fieldInfo.GetValue(this) != null)
							{
								stringBuilder.AppendLine(fieldInfo.Name);
								foreach (string text in (obj as IStatistics).ToString().Split(new char[] { '\n' }))
								{
									stringBuilder.AppendFormat("  {0}\n", text);
								}
							}
						}
						else
						{
							stringBuilder.AppendLine(string.Format(CultureInfo.CurrentUICulture, "{0}: {1}", new object[]
							{
								fieldInfo.Name,
								fieldInfo.GetValue(this)
							}));
						}
					}
				}
				else if (obj is PropertyInfo)
				{
					PropertyInfo propertyInfo = (PropertyInfo)obj;
					object[] customAttributes2 = propertyInfo.GetCustomAttributes(typeof(StatisticAttribute), false);
					if (customAttributes2 == null || customAttributes2.Length == 0 || !(customAttributes2[0] as StatisticAttribute).Ignore)
					{
						if (typeof(Stopwatch) == propertyInfo.PropertyType)
						{
							if (this.EnableTimers)
							{
								stringBuilder.AppendLine(string.Format(CultureInfo.CurrentUICulture, "{0}: {1:F2} ms", new object[]
								{
									propertyInfo.Name,
									((Stopwatch)propertyInfo.GetValue(this, null)).Elapsed.TotalMilliseconds
								}));
							}
						}
						else if (typeof(IStatistics) == propertyInfo.PropertyType)
						{
							if ((IStatistics)propertyInfo.GetValue(this, null) != null)
							{
								stringBuilder.AppendLine(propertyInfo.Name);
								foreach (string text2 in (obj as IStatistics).ToString().Split(new char[] { '\n' }))
								{
									stringBuilder.AppendFormat("  {0}\n", text2);
								}
							}
						}
						else
						{
							stringBuilder.AppendLine(string.Format(CultureInfo.CurrentUICulture, "{0}: {1}", new object[]
							{
								propertyInfo.Name,
								propertyInfo.GetValue(this, null)
							}));
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00013BE8 File Offset: 0x00011DE8
		public virtual void Reset()
		{
			foreach (FieldInfo fieldInfo in base.GetType().GetFields())
			{
				if (typeof(long) == fieldInfo.FieldType)
				{
					fieldInfo.SetValue(this, 0L);
				}
				else if (typeof(double) == fieldInfo.FieldType)
				{
					fieldInfo.SetValue(this, 0.0);
				}
				else if (typeof(Stopwatch) == fieldInfo.FieldType)
				{
					((Stopwatch)fieldInfo.GetValue(this)).Reset();
				}
			}
		}

		// Token: 0x04000067 RID: 103
		protected List<object> m_fields = new List<object>();
	}
}
