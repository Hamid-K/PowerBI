using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000305 RID: 773
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DynamicLoaderException : MonitoredException
	{
		// Token: 0x06001517 RID: 5399 RVA: 0x0004AABC File Offset: 0x00048CBC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0004AAC4 File Offset: 0x00048CC4
		// (set) Token: 0x06001519 RID: 5401 RVA: 0x0004AACC File Offset: 0x00048CCC
		public string Assembly
		{
			get
			{
				return this.m_assembly;
			}
			protected set
			{
				this.m_assembly = value;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0004AAD5 File Offset: 0x00048CD5
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x0004AADD File Offset: 0x00048CDD
		public string LoadType
		{
			get
			{
				return this.m_loadType;
			}
			protected set
			{
				this.m_loadType = value;
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0004AAE6 File Offset: 0x00048CE6
		public DynamicLoaderException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0004AAFF File Offset: 0x00048CFF
		public DynamicLoaderException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0004AB16 File Offset: 0x00048D16
		public DynamicLoaderException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0004AB34 File Offset: 0x00048D34
		protected DynamicLoaderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DynamicLoaderException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Assembly = (string)info.GetValue("DynamicLoaderException_Assembly", typeof(string));
			}
			catch (SerializationException)
			{
				this.Assembly = null;
			}
			try
			{
				this.LoadType = (string)info.GetValue("DynamicLoaderException_LoadType", typeof(string));
			}
			catch (SerializationException)
			{
				this.LoadType = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DynamicLoaderException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0004AC44 File Offset: 0x00048E44
		public DynamicLoaderException(string assembly, string loadType)
		{
			this.Assembly = assembly;
			this.LoadType = loadType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0004AC61 File Offset: 0x00048E61
		public DynamicLoaderException(string assembly, string loadType, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Assembly = assembly;
			this.LoadType = loadType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0004AC86 File Offset: 0x00048E86
		public DynamicLoaderException(string assembly, string loadType, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Assembly = assembly;
			this.LoadType = loadType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0004ACB4 File Offset: 0x00048EB4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0004ACEB File Offset: 0x00048EEB
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0004ACF4 File Offset: 0x00048EF4
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DynamicLoaderException))
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

		// Token: 0x06001526 RID: 5414 RVA: 0x0004ADC4 File Offset: 0x00048FC4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DynamicLoaderException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DynamicLoaderException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Assembly != null)
			{
				info.AddValue("DynamicLoaderException_Assembly", this.Assembly, typeof(string));
			}
			if (this.LoadType != null)
			{
				info.AddValue("DynamicLoaderException_LoadType", this.LoadType, typeof(string));
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0004AE68 File Offset: 0x00049068
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to load type '{0}' from assembly '{1}'", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.LoadType != null) ? this.LoadType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.LoadType != null) ? this.LoadType.MarkIfInternal() : string.Empty) : ((this.LoadType != null) ? this.LoadType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Assembly != null) ? this.Assembly.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Assembly != null) ? this.Assembly.MarkIfInternal() : string.Empty) : ((this.Assembly != null) ? this.Assembly.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0004AF4E File Offset: 0x0004914E
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

		// Token: 0x06001529 RID: 5417 RVA: 0x0004AF6C File Offset: 0x0004916C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Assembly={0}", new object[] { (this.Assembly != null) ? this.Assembly.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Assembly={0}", new object[] { (this.Assembly != null) ? this.Assembly.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Assembly={0}", new object[] { (this.Assembly != null) ? this.Assembly.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "LoadType={0}", new object[] { (this.LoadType != null) ? this.LoadType.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "LoadType={0}", new object[] { (this.LoadType != null) ? this.LoadType.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "LoadType={0}", new object[] { (this.LoadType != null) ? this.LoadType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0004B10E File Offset: 0x0004930E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0004B117 File Offset: 0x00049317
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0004B120 File Offset: 0x00049320
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0004B10E File Offset: 0x0004930E
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0004B12C File Offset: 0x0004932C
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

		// Token: 0x040007D1 RID: 2001
		private string creationMessage;

		// Token: 0x040007D2 RID: 2002
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007D3 RID: 2003
		private string m_assembly;

		// Token: 0x040007D4 RID: 2004
		private string m_loadType;
	}
}
