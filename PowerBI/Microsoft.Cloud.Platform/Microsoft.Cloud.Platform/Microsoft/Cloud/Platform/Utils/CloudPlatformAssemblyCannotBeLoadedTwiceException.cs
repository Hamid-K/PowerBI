using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002F9 RID: 761
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CloudPlatformAssemblyCannotBeLoadedTwiceException : MonitoredException
	{
		// Token: 0x0600142E RID: 5166 RVA: 0x000464AE File Offset: 0x000446AE
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x000464B6 File Offset: 0x000446B6
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x000464BE File Offset: 0x000446BE
		public string OriginalLocation
		{
			get
			{
				return this.m_originalLocation;
			}
			protected set
			{
				this.m_originalLocation = value;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x000464C7 File Offset: 0x000446C7
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x000464CF File Offset: 0x000446CF
		public string SecondLocation
		{
			get
			{
				return this.m_secondLocation;
			}
			protected set
			{
				this.m_secondLocation = value;
			}
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x000464D8 File Offset: 0x000446D8
		public CloudPlatformAssemblyCannotBeLoadedTwiceException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x000464F1 File Offset: 0x000446F1
		public CloudPlatformAssemblyCannotBeLoadedTwiceException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00046508 File Offset: 0x00044708
		public CloudPlatformAssemblyCannotBeLoadedTwiceException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00046528 File Offset: 0x00044728
		protected CloudPlatformAssemblyCannotBeLoadedTwiceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CloudPlatformAssemblyCannotBeLoadedTwiceException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.OriginalLocation = (string)info.GetValue("CloudPlatformAssemblyCannotBeLoadedTwiceException_OriginalLocation", typeof(string));
			}
			catch (SerializationException)
			{
				this.OriginalLocation = null;
			}
			try
			{
				this.SecondLocation = (string)info.GetValue("CloudPlatformAssemblyCannotBeLoadedTwiceException_SecondLocation", typeof(string));
			}
			catch (SerializationException)
			{
				this.SecondLocation = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CloudPlatformAssemblyCannotBeLoadedTwiceException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00046638 File Offset: 0x00044838
		public CloudPlatformAssemblyCannotBeLoadedTwiceException(string originalLocation, string secondLocation)
		{
			this.OriginalLocation = originalLocation;
			this.SecondLocation = secondLocation;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00046655 File Offset: 0x00044855
		public CloudPlatformAssemblyCannotBeLoadedTwiceException(string originalLocation, string secondLocation, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.OriginalLocation = originalLocation;
			this.SecondLocation = secondLocation;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0004667A File Offset: 0x0004487A
		public CloudPlatformAssemblyCannotBeLoadedTwiceException(string originalLocation, string secondLocation, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.OriginalLocation = originalLocation;
			this.SecondLocation = secondLocation;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x000466A8 File Offset: 0x000448A8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x000466DF File Offset: 0x000448DF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x000034FD File Offset: 0x000016FD
		public override bool IsFatal()
		{
			return true;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x000466E8 File Offset: 0x000448E8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CloudPlatformAssemblyCannotBeLoadedTwiceException))
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

		// Token: 0x0600143E RID: 5182 RVA: 0x000467B8 File Offset: 0x000449B8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CloudPlatformAssemblyCannotBeLoadedTwiceException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CloudPlatformAssemblyCannotBeLoadedTwiceException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.OriginalLocation != null)
			{
				info.AddValue("CloudPlatformAssemblyCannotBeLoadedTwiceException_OriginalLocation", this.OriginalLocation, typeof(string));
			}
			if (this.SecondLocation != null)
			{
				info.AddValue("CloudPlatformAssemblyCannotBeLoadedTwiceException_SecondLocation", this.SecondLocation, typeof(string));
			}
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0004685C File Offset: 0x00044A5C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The Microsoft.Cloud.Platform assembly has been loaded a second time from a different location, which is a bug. Initially loaded from: '{0}'; Second location: '{1}'", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.OriginalLocation != null) ? this.OriginalLocation.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.OriginalLocation != null) ? this.OriginalLocation.MarkIfInternal() : string.Empty) : ((this.OriginalLocation != null) ? this.OriginalLocation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.SecondLocation != null) ? this.SecondLocation.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.SecondLocation != null) ? this.SecondLocation.MarkIfInternal() : string.Empty) : ((this.SecondLocation != null) ? this.SecondLocation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00046942 File Offset: 0x00044B42
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

		// Token: 0x06001441 RID: 5185 RVA: 0x00046960 File Offset: 0x00044B60
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "OriginalLocation={0}", new object[] { (this.OriginalLocation != null) ? this.OriginalLocation.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "OriginalLocation={0}", new object[] { (this.OriginalLocation != null) ? this.OriginalLocation.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "OriginalLocation={0}", new object[] { (this.OriginalLocation != null) ? this.OriginalLocation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "SecondLocation={0}", new object[] { (this.SecondLocation != null) ? this.SecondLocation.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "SecondLocation={0}", new object[] { (this.SecondLocation != null) ? this.SecondLocation.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "SecondLocation={0}", new object[] { (this.SecondLocation != null) ? this.SecondLocation.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00046B02 File Offset: 0x00044D02
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00046B0B File Offset: 0x00044D0B
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00046B14 File Offset: 0x00044D14
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00046B02 File Offset: 0x00044D02
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00046B20 File Offset: 0x00044D20
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

		// Token: 0x040007B2 RID: 1970
		private string creationMessage;

		// Token: 0x040007B3 RID: 1971
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007B4 RID: 1972
		private string m_originalLocation;

		// Token: 0x040007B5 RID: 1973
		private string m_secondLocation;
	}
}
