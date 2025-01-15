using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001EF RID: 495
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class LoadableObjectNotInstantiatedException : DynamicLoaderException
	{
		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002D0A1 File Offset: 0x0002B2A1
		internal LoadableObjectNotInstantiatedException(string assembly, string type, Exception innerException)
			: base(assembly, type, null, innerException)
		{
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0002D668 File Offset: 0x0002B868
		internal LoadableObjectNotInstantiatedException(Type type, Exception innerException)
			: base(type.Assembly.FullName, type.FullName, null, innerException)
		{
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0002D088 File Offset: 0x0002B288
		internal LoadableObjectNotInstantiatedException(Type type)
			: base(type.Assembly.FullName, type.FullName)
		{
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002D683 File Offset: 0x0002B883
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002D68B File Offset: 0x0002B88B
		public LoadableObjectNotInstantiatedException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002D69A File Offset: 0x0002B89A
		public LoadableObjectNotInstantiatedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002D6B1 File Offset: 0x0002B8B1
		public LoadableObjectNotInstantiatedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002D6D0 File Offset: 0x0002B8D0
		protected LoadableObjectNotInstantiatedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("LoadableObjectNotInstantiatedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("LoadableObjectNotInstantiatedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002D76C File Offset: 0x0002B96C
		public LoadableObjectNotInstantiatedException(string assembly, string loadType)
			: base(assembly, loadType)
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0002D77D File Offset: 0x0002B97D
		public LoadableObjectNotInstantiatedException(string assembly, string loadType, string message, Exception innerException)
			: base(assembly, loadType, message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0002D79D File Offset: 0x0002B99D
		public LoadableObjectNotInstantiatedException(string assembly, string loadType, string message)
			: base(assembly, loadType, message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002D7B8 File Offset: 0x0002B9B8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002D7EF File Offset: 0x0002B9EF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002D7F8 File Offset: 0x0002B9F8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(LoadableObjectNotInstantiatedException))
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

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002D8C8 File Offset: 0x0002BAC8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("LoadableObjectNotInstantiatedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("LoadableObjectNotInstantiatedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002D924 File Offset: 0x0002BB24
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Type '{0}' cannot be loaded from assembly '{1}'", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((base.LoadType != null) ? base.LoadType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((base.LoadType != null) ? base.LoadType.MarkIfInternal() : string.Empty) : ((base.LoadType != null) ? base.LoadType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((base.Assembly != null) ? base.Assembly.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((base.Assembly != null) ? base.Assembly.MarkIfInternal() : string.Empty) : ((base.Assembly != null) ? base.Assembly.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0002DA0A File Offset: 0x0002BC0A
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

		// Token: 0x06000D04 RID: 3332 RVA: 0x0002D44F File Offset: 0x0002B64F
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002DA27 File Offset: 0x0002BC27
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0002DA30 File Offset: 0x0002BC30
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0002DA39 File Offset: 0x0002BC39
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002DA27 File Offset: 0x0002BC27
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0002DA44 File Offset: 0x0002BC44
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

		// Token: 0x04000501 RID: 1281
		private string creationMessage;

		// Token: 0x04000502 RID: 1282
		private ExceptionCulprit exceptionCulprit;
	}
}
