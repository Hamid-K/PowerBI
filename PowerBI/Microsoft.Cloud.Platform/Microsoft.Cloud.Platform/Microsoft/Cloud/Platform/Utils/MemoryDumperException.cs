using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000304 RID: 772
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class MemoryDumperException : MonitoredException
	{
		// Token: 0x06001502 RID: 5378 RVA: 0x0004A424 File Offset: 0x00048624
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0004A42C File Offset: 0x0004862C
		// (set) Token: 0x06001504 RID: 5380 RVA: 0x0004A434 File Offset: 0x00048634
		public string ProcessIdentifier
		{
			get
			{
				return this.m_processIdentifier;
			}
			protected set
			{
				this.m_processIdentifier = value;
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0004A43D File Offset: 0x0004863D
		public MemoryDumperException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0004A451 File Offset: 0x00048651
		public MemoryDumperException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0004A468 File Offset: 0x00048668
		public MemoryDumperException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0004A488 File Offset: 0x00048688
		protected MemoryDumperException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MemoryDumperException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ProcessIdentifier = (string)info.GetValue("MemoryDumperException_ProcessIdentifier", typeof(string));
			}
			catch (SerializationException)
			{
				this.ProcessIdentifier = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("MemoryDumperException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0004A55C File Offset: 0x0004875C
		public MemoryDumperException(string processIdentifier, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ProcessIdentifier = processIdentifier;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0004A57A File Offset: 0x0004877A
		public MemoryDumperException(string processIdentifier, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ProcessIdentifier = processIdentifier;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0004A5A0 File Offset: 0x000487A0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0004A5D7 File Offset: 0x000487D7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0004A5E0 File Offset: 0x000487E0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MemoryDumperException))
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

		// Token: 0x0600150E RID: 5390 RVA: 0x0004A6B0 File Offset: 0x000488B0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MemoryDumperException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("MemoryDumperException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ProcessIdentifier != null)
			{
				info.AddValue("MemoryDumperException_ProcessIdentifier", this.ProcessIdentifier, typeof(string));
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0004A730 File Offset: 0x00048930
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed taking a memory dump of process '{0}'", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.ProcessIdentifier != null) ? this.ProcessIdentifier.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ProcessIdentifier != null) ? this.ProcessIdentifier.MarkIfInternal() : string.Empty) : ((this.ProcessIdentifier != null) ? this.ProcessIdentifier.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0004A7B4 File Offset: 0x000489B4
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

		// Token: 0x06001511 RID: 5393 RVA: 0x0004A7D4 File Offset: 0x000489D4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ProcessIdentifier={0}", new object[] { (this.ProcessIdentifier != null) ? this.ProcessIdentifier.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ProcessIdentifier={0}", new object[] { (this.ProcessIdentifier != null) ? this.ProcessIdentifier.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ProcessIdentifier={0}", new object[] { (this.ProcessIdentifier != null) ? this.ProcessIdentifier.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0004A8B3 File Offset: 0x00048AB3
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0004A8BC File Offset: 0x00048ABC
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0004A8C5 File Offset: 0x00048AC5
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0004A8B3 File Offset: 0x00048AB3
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0004A8D0 File Offset: 0x00048AD0
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

		// Token: 0x040007CE RID: 1998
		private string creationMessage;

		// Token: 0x040007CF RID: 1999
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007D0 RID: 2000
		private string m_processIdentifier;
	}
}
