using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200030C RID: 780
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class QueueFullException : MonitoredException
	{
		// Token: 0x060015A4 RID: 5540 RVA: 0x0004D490 File Offset: 0x0004B690
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0004D498 File Offset: 0x0004B698
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x0004D4A0 File Offset: 0x0004B6A0
		public int Capacity
		{
			get
			{
				return this.m_capacity;
			}
			protected set
			{
				this.m_capacity = value;
			}
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0004D4A9 File Offset: 0x0004B6A9
		public QueueFullException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<int>();
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0004D4BD File Offset: 0x0004B6BD
		public QueueFullException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0004D4D4 File Offset: 0x0004B6D4
		public QueueFullException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0004D4F4 File Offset: 0x0004B6F4
		protected QueueFullException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("QueueFullException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.Capacity = (int)info.GetValue("QueueFullException_Capacity", typeof(int));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("QueueFullException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0004D5B0 File Offset: 0x0004B7B0
		public QueueFullException(int capacity)
		{
			this.Capacity = capacity;
			this.ConstructorInternal(false);
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0004D5C6 File Offset: 0x0004B7C6
		public QueueFullException(int capacity, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Capacity = capacity;
			this.ConstructorInternal(false);
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0004D5E4 File Offset: 0x0004B7E4
		public QueueFullException(int capacity, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Capacity = capacity;
			this.ConstructorInternal(false);
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0004D608 File Offset: 0x0004B808
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0004D63F File Offset: 0x0004B83F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0004D648 File Offset: 0x0004B848
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(QueueFullException))
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<UtilsTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0004D718 File Offset: 0x0004B918
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("QueueFullException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("QueueFullException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("QueueFullException_Capacity", this.Capacity, typeof(int));
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0004CDC3 File Offset: 0x0004AFC3
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "", new object[0]);
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x0004D793 File Offset: 0x0004B993
		public override string Message
		{
			get
			{
				if (!string.IsNullOrEmpty(this.creationMessage))
				{
					return this.creationMessage;
				}
				return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.None);
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0004D7B0 File Offset: 0x0004B9B0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Capacity={0}", new object[] { this.Capacity.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Capacity={0}", new object[] { this.Capacity.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "Capacity={0}", new object[] { this.Capacity.ToString(CultureInfo.InvariantCulture) })));
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0004D874 File Offset: 0x0004BA74
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0004D87D File Offset: 0x0004BA7D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0004D886 File Offset: 0x0004BA86
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0004D874 File Offset: 0x0004BA74
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0004D890 File Offset: 0x0004BA90
		private string ToString(PrivateInformationMarkupKind markupKind)
		{
			string text = "[" + ExceptionsTemplateHelper.MagicLevel.ToString(CultureInfo.CurrentCulture) + "]" + base.GetType().FullName;
			string text2 = this.CreateMessageFromTemplate(markupKind);
			string text3 = text + ": ";
			if (string.IsNullOrEmpty(this.creationMessage))
			{
				text3 += text2;
			}
			else
			{
				if (markupKind == PrivateInformationMarkupKind.Private || markupKind == PrivateInformationMarkupKind.Internal)
				{
					text3 += this.creationMessage.ObfuscatePrivateValue(true);
				}
				else
				{
					text3 += this.creationMessage;
				}
				if (!string.Equals(this.creationMessage, text2))
				{
					text3 = text3 + Environment.NewLine + "  TemplateMessage: " + text2;
				}
			}
			text3 += this.GetPropertiesString(markupKind);
			text3 = text3 + Environment.NewLine + "ExceptionCulprit=" + this.exceptionCulprit.ToString();
			if (base.InnerException != null)
			{
				try
				{
					ExceptionsTemplateHelper.IncrementMagicLevel();
					IMonitoredError monitoredError = base.InnerException as MonitoredException;
					string text4;
					if (markupKind != PrivateInformationMarkupKind.None)
					{
						if (markupKind != PrivateInformationMarkupKind.Internal)
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfPrivate() : monitoredError.ToPrivateString());
							text4 = text4.ObfuscatePrivateValue(true);
						}
						else
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfInternal() : monitoredError.ToInternalString());
						}
					}
					else
					{
						text4 = ((monitoredError == null) ? base.InnerException.ToString() : monitoredError.ToOriginalString());
					}
					text3 = string.Concat(new string[]
					{
						text3,
						" --->",
						Environment.NewLine,
						text4,
						Environment.NewLine,
						"   --- End of inner exception stack trace ---",
						Environment.NewLine,
						"  (",
						text,
						".StackTrace:)"
					});
				}
				finally
				{
					ExceptionsTemplateHelper.DecrementMagicLevel();
				}
			}
			if (this.StackTrace != null)
			{
				text3 = text3 + Environment.NewLine + this.StackTrace;
			}
			return text3;
		}

		// Token: 0x040007E5 RID: 2021
		private string creationMessage;

		// Token: 0x040007E6 RID: 2022
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007E7 RID: 2023
		private int m_capacity;
	}
}
