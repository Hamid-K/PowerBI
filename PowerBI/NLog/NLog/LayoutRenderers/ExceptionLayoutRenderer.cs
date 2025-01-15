using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.MessageTemplates;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C2 RID: 194
	[LayoutRenderer("exception")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class ExceptionLayoutRenderer : LayoutRenderer, IRawValue
	{
		// Token: 0x06000C1E RID: 3102 RVA: 0x0001F2D8 File Offset: 0x0001D4D8
		public ExceptionLayoutRenderer()
		{
			this.Format = "message";
			this.Separator = " ";
			this.ExceptionDataSeparator = ";";
			this.InnerExceptionSeparator = EnvironmentHelper.NewLine;
			this.MaxInnerExceptionLevel = 0;
			this._renderingfunctions = new Dictionary<ExceptionRenderingFormat, Action<StringBuilder, Exception>>
			{
				{
					ExceptionRenderingFormat.Message,
					new Action<StringBuilder, Exception>(this.AppendMessage)
				},
				{
					ExceptionRenderingFormat.Type,
					new Action<StringBuilder, Exception>(this.AppendType)
				},
				{
					ExceptionRenderingFormat.ShortType,
					new Action<StringBuilder, Exception>(this.AppendShortType)
				},
				{
					ExceptionRenderingFormat.ToString,
					new Action<StringBuilder, Exception>(this.AppendToString)
				},
				{
					ExceptionRenderingFormat.Method,
					new Action<StringBuilder, Exception>(this.AppendMethod)
				},
				{
					ExceptionRenderingFormat.StackTrace,
					new Action<StringBuilder, Exception>(this.AppendStackTrace)
				},
				{
					ExceptionRenderingFormat.Data,
					new Action<StringBuilder, Exception>(this.AppendData)
				},
				{
					ExceptionRenderingFormat.Serialize,
					new Action<StringBuilder, Exception>(this.AppendSerializeObject)
				}
			};
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0001F3D4 File Offset: 0x0001D5D4
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0001F3DC File Offset: 0x0001D5DC
		[DefaultParameter]
		public string Format
		{
			get
			{
				return this._format;
			}
			set
			{
				this._format = value;
				this.Formats = ExceptionLayoutRenderer.CompileFormat(value);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0001F3F1 File Offset: 0x0001D5F1
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x0001F3F9 File Offset: 0x0001D5F9
		public string InnerFormat
		{
			get
			{
				return this._innerFormat;
			}
			set
			{
				this._innerFormat = value;
				this.InnerFormats = ExceptionLayoutRenderer.CompileFormat(value);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0001F40E File Offset: 0x0001D60E
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0001F416 File Offset: 0x0001D616
		[DefaultValue(" ")]
		public string Separator { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0001F41F File Offset: 0x0001D61F
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0001F427 File Offset: 0x0001D627
		[DefaultValue(";")]
		public string ExceptionDataSeparator { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0001F430 File Offset: 0x0001D630
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0001F438 File Offset: 0x0001D638
		[DefaultValue(0)]
		public int MaxInnerExceptionLevel { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0001F441 File Offset: 0x0001D641
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0001F449 File Offset: 0x0001D649
		public string InnerExceptionSeparator { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0001F452 File Offset: 0x0001D652
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x0001F45A File Offset: 0x0001D65A
		public List<ExceptionRenderingFormat> Formats { get; private set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0001F463 File Offset: 0x0001D663
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x0001F46B File Offset: 0x0001D66B
		public List<ExceptionRenderingFormat> InnerFormats { get; private set; }

		// Token: 0x06000C2F RID: 3119 RVA: 0x0001F474 File Offset: 0x0001D674
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = logEvent.Exception;
			return true;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0001F480 File Offset: 0x0001D680
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			Exception ex = logEvent.Exception;
			if (ex != null)
			{
				int num = 0;
				AggregateException ex2;
				if ((ex2 = logEvent.Exception as AggregateException) != null)
				{
					ex2 = ex2.Flatten();
					ex = ExceptionLayoutRenderer.GetPrimaryException(ex2);
					this.AppendException(ex, this.Formats, builder);
					if (num < this.MaxInnerExceptionLevel)
					{
						num = this.AppendInnerExceptionTree(ex, num, builder);
						if (num < this.MaxInnerExceptionLevel)
						{
							ReadOnlyCollection<Exception> innerExceptions = ex2.InnerExceptions;
							if (innerExceptions != null && innerExceptions.Count > 1)
							{
								this.AppendAggregateException(ex2, num, builder);
								return;
							}
						}
					}
				}
				else
				{
					this.AppendException(ex, this.Formats, builder);
					if (num < this.MaxInnerExceptionLevel)
					{
						this.AppendInnerExceptionTree(ex, num, builder);
					}
				}
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0001F525 File Offset: 0x0001D725
		private static Exception GetPrimaryException(AggregateException aggregateException)
		{
			if (aggregateException.InnerExceptions.Count != 1)
			{
				return aggregateException;
			}
			return aggregateException.InnerExceptions[0];
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0001F544 File Offset: 0x0001D744
		private void AppendAggregateException(AggregateException primaryException, int currentLevel, StringBuilder builder)
		{
			AggregateException ex = primaryException.Flatten();
			if (ex.InnerExceptions != null)
			{
				int num = 0;
				while (num < ex.InnerExceptions.Count && currentLevel < this.MaxInnerExceptionLevel)
				{
					Exception ex2 = ex.InnerExceptions[num];
					if (ex2 != primaryException.InnerException)
					{
						if (ex2 == null)
						{
							InternalLogger.Debug("Skipping rendering exception as exception is null");
						}
						else
						{
							this.AppendInnerException(ex2, builder);
							currentLevel++;
							currentLevel = this.AppendInnerExceptionTree(ex2, currentLevel, builder);
						}
					}
					num++;
					currentLevel++;
				}
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0001F5C1 File Offset: 0x0001D7C1
		private int AppendInnerExceptionTree(Exception currentException, int currentLevel, StringBuilder sb)
		{
			currentException = currentException.InnerException;
			while (currentException != null && currentLevel < this.MaxInnerExceptionLevel)
			{
				this.AppendInnerException(currentException, sb);
				currentLevel++;
				currentException = currentException.InnerException;
			}
			return currentLevel;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0001F5EF File Offset: 0x0001D7EF
		private void AppendInnerException(Exception currentException, StringBuilder builder)
		{
			builder.Append(this.InnerExceptionSeparator);
			this.AppendException(currentException, this.InnerFormats ?? this.Formats, builder);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0001F618 File Offset: 0x0001D818
		private void AppendException(Exception currentException, IEnumerable<ExceptionRenderingFormat> renderFormats, StringBuilder builder)
		{
			int num = builder.Length;
			foreach (ExceptionRenderingFormat exceptionRenderingFormat in renderFormats)
			{
				if (num != builder.Length)
				{
					num = builder.Length;
					builder.Append(this.Separator);
				}
				int length = builder.Length;
				this._renderingfunctions[exceptionRenderingFormat](builder, currentException);
				if (builder.Length == length && builder.Length != num)
				{
					builder.Length = num;
				}
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0001F6B0 File Offset: 0x0001D8B0
		protected virtual void AppendMessage(StringBuilder sb, Exception ex)
		{
			try
			{
				sb.Append(ex.Message);
			}
			catch (Exception ex2)
			{
				string text = string.Concat(new string[]
				{
					"Exception in ",
					typeof(ExceptionLayoutRenderer).FullName,
					".AppendMessage(): ",
					ex2.GetType().FullName,
					"."
				});
				sb.Append("NLog message: ");
				sb.Append(text);
				InternalLogger.Warn(ex2, text);
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0001F740 File Offset: 0x0001D940
		protected virtual void AppendMethod(StringBuilder sb, Exception ex)
		{
			if (ex.TargetSite != null)
			{
				sb.Append(ex.TargetSite.ToString());
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0001F762 File Offset: 0x0001D962
		protected virtual void AppendStackTrace(StringBuilder sb, Exception ex)
		{
			if (!string.IsNullOrEmpty(ex.StackTrace))
			{
				sb.Append(ex.StackTrace);
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0001F77E File Offset: 0x0001D97E
		protected virtual void AppendToString(StringBuilder sb, Exception ex)
		{
			sb.Append(ex.ToString());
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0001F78D File Offset: 0x0001D98D
		protected virtual void AppendType(StringBuilder sb, Exception ex)
		{
			sb.Append(ex.GetType().FullName);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0001F7A1 File Offset: 0x0001D9A1
		protected virtual void AppendShortType(StringBuilder sb, Exception ex)
		{
			sb.Append(ex.GetType().Name);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
		protected virtual void AppendData(StringBuilder sb, Exception ex)
		{
			if (ex.Data != null && ex.Data.Count > 0)
			{
				string text = string.Empty;
				foreach (object obj in ex.Data.Keys)
				{
					sb.Append(text);
					sb.AppendFormat("{0}: {1}", obj, ex.Data[obj]);
					text = this.ExceptionDataSeparator;
				}
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0001F850 File Offset: 0x0001DA50
		protected virtual void AppendSerializeObject(StringBuilder sb, Exception ex)
		{
			ConfigurationItemFactory.Default.ValueFormatter.FormatValue(ex, null, CaptureType.Serialize, null, sb);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0001F868 File Offset: 0x0001DA68
		private static List<ExceptionRenderingFormat> CompileFormat(string formatSpecifier)
		{
			List<ExceptionRenderingFormat> list = new List<ExceptionRenderingFormat>();
			foreach (string text in formatSpecifier.SplitAndTrimTokens(','))
			{
				ExceptionRenderingFormat exceptionRenderingFormat;
				if (ExceptionLayoutRenderer._formatsMapping.TryGetValue(text, out exceptionRenderingFormat))
				{
					list.Add(exceptionRenderingFormat);
				}
				else
				{
					InternalLogger.Warn<string>("Unknown exception data target: {0}", text);
				}
			}
			return list;
		}

		// Token: 0x040002FC RID: 764
		private string _format;

		// Token: 0x040002FD RID: 765
		private string _innerFormat = string.Empty;

		// Token: 0x040002FE RID: 766
		private readonly Dictionary<ExceptionRenderingFormat, Action<StringBuilder, Exception>> _renderingfunctions;

		// Token: 0x040002FF RID: 767
		private static readonly Dictionary<string, ExceptionRenderingFormat> _formatsMapping = new Dictionary<string, ExceptionRenderingFormat>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"MESSAGE",
				ExceptionRenderingFormat.Message
			},
			{
				"TYPE",
				ExceptionRenderingFormat.Type
			},
			{
				"SHORTTYPE",
				ExceptionRenderingFormat.ShortType
			},
			{
				"TOSTRING",
				ExceptionRenderingFormat.ToString
			},
			{
				"METHOD",
				ExceptionRenderingFormat.Method
			},
			{
				"STACKTRACE",
				ExceptionRenderingFormat.StackTrace
			},
			{
				"DATA",
				ExceptionRenderingFormat.Data
			},
			{
				"@",
				ExceptionRenderingFormat.Serialize
			}
		};
	}
}
