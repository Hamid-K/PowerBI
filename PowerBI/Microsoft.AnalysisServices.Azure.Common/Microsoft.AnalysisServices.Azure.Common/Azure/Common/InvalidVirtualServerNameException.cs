using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000DB RID: 219
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class InvalidVirtualServerNameException : MonitoredException
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x0001FC9C File Offset: 0x0001DE9C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		public string VirtualServerName
		{
			get
			{
				return this.m_virtualServerName;
			}
			protected set
			{
				this.m_virtualServerName = value;
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001FCB5 File Offset: 0x0001DEB5
		public InvalidVirtualServerNameException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001FCC9 File Offset: 0x0001DEC9
		public InvalidVirtualServerNameException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001FCE0 File Offset: 0x0001DEE0
		public InvalidVirtualServerNameException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001FD00 File Offset: 0x0001DF00
		protected InvalidVirtualServerNameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidVirtualServerNameException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.VirtualServerName = (string)info.GetValue("InvalidVirtualServerNameException_VirtualServerName", typeof(string));
			}
			catch (SerializationException)
			{
				this.VirtualServerName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("InvalidVirtualServerNameException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
		public InvalidVirtualServerNameException(string virtualServerName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.VirtualServerName = virtualServerName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001FDF2 File Offset: 0x0001DFF2
		public InvalidVirtualServerNameException(string virtualServerName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.VirtualServerName = virtualServerName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001FE18 File Offset: 0x0001E018
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001FE4F File Offset: 0x0001E04F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001FE58 File Offset: 0x0001E058
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidVirtualServerNameException))
			{
				TraceSourceBase<ANCommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ANCommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001FF28 File Offset: 0x0001E128
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidVirtualServerNameException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("InvalidVirtualServerNameException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.VirtualServerName != null)
			{
				info.AddValue("InvalidVirtualServerNameException_VirtualServerName", this.VirtualServerName, typeof(string));
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001FFA8 File Offset: 0x0001E1A8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The provided virtual server name {0} is invalid", (markupKind == PrivateInformationMarkupKind.None) ? ((this.VirtualServerName != null) ? this.VirtualServerName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.VirtualServerName != null) ? this.VirtualServerName.MarkIfInternal() : string.Empty) : ((this.VirtualServerName != null) ? this.VirtualServerName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00020023 File Offset: 0x0001E223
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

		// Token: 0x0600094F RID: 2383 RVA: 0x00020040 File Offset: 0x0001E240
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "VirtualServerName={0}", (this.VirtualServerName != null) ? this.VirtualServerName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "VirtualServerName={0}", (this.VirtualServerName != null) ? this.VirtualServerName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "VirtualServerName={0}", (this.VirtualServerName != null) ? this.VirtualServerName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00020104 File Offset: 0x0001E304
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002010D File Offset: 0x0001E30D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00020116 File Offset: 0x0001E316
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00020104 File Offset: 0x0001E304
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00020120 File Offset: 0x0001E320
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

		// Token: 0x040002A1 RID: 673
		private string creationMessage;

		// Token: 0x040002A2 RID: 674
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040002A3 RID: 675
		private string m_virtualServerName;
	}
}
