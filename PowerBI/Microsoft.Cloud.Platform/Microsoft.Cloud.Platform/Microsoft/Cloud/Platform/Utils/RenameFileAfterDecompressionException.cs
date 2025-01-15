using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200031F RID: 799
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class RenameFileAfterDecompressionException : CompressionException
	{
		// Token: 0x0600171D RID: 5917 RVA: 0x00054504 File Offset: 0x00052704
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x0005450C File Offset: 0x0005270C
		// (set) Token: 0x0600171F RID: 5919 RVA: 0x00054514 File Offset: 0x00052714
		public string TempDecompressed
		{
			get
			{
				return this.m_tempDecompressed;
			}
			protected set
			{
				this.m_tempDecompressed = value;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x0005451D File Offset: 0x0005271D
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x00054525 File Offset: 0x00052725
		public string TargetDecompressed
		{
			get
			{
				return this.m_targetDecompressed;
			}
			protected set
			{
				this.m_targetDecompressed = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x0005452E File Offset: 0x0005272E
		// (set) Token: 0x06001723 RID: 5923 RVA: 0x00054536 File Offset: 0x00052736
		public string Compressed
		{
			get
			{
				return this.m_compressed;
			}
			protected set
			{
				this.m_compressed = value;
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0005453F File Offset: 0x0005273F
		public RenameFileAfterDecompressionException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0005455D File Offset: 0x0005275D
		public RenameFileAfterDecompressionException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00054574 File Offset: 0x00052774
		public RenameFileAfterDecompressionException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00054594 File Offset: 0x00052794
		protected RenameFileAfterDecompressionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("RenameFileAfterDecompressionException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.TempDecompressed = (string)info.GetValue("RenameFileAfterDecompressionException_TempDecompressed", typeof(string));
			}
			catch (SerializationException)
			{
				this.TempDecompressed = null;
			}
			try
			{
				this.TargetDecompressed = (string)info.GetValue("RenameFileAfterDecompressionException_TargetDecompressed", typeof(string));
			}
			catch (SerializationException)
			{
				this.TargetDecompressed = null;
			}
			try
			{
				this.Compressed = (string)info.GetValue("RenameFileAfterDecompressionException_Compressed", typeof(string));
			}
			catch (SerializationException)
			{
				this.Compressed = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("RenameFileAfterDecompressionException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x000546DC File Offset: 0x000528DC
		public RenameFileAfterDecompressionException(string tempDecompressed, string targetDecompressed, string compressed)
		{
			this.TempDecompressed = tempDecompressed;
			this.TargetDecompressed = targetDecompressed;
			this.Compressed = compressed;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00054700 File Offset: 0x00052900
		public RenameFileAfterDecompressionException(string tempDecompressed, string targetDecompressed, string compressed, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.TempDecompressed = tempDecompressed;
			this.TargetDecompressed = targetDecompressed;
			this.Compressed = compressed;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0005472E File Offset: 0x0005292E
		public RenameFileAfterDecompressionException(string tempDecompressed, string targetDecompressed, string compressed, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.TempDecompressed = tempDecompressed;
			this.TargetDecompressed = targetDecompressed;
			this.Compressed = compressed;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00054764 File Offset: 0x00052964
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0005479B File Offset: 0x0005299B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000547A4 File Offset: 0x000529A4
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(RenameFileAfterDecompressionException))
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

		// Token: 0x0600172E RID: 5934 RVA: 0x00054874 File Offset: 0x00052A74
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("RenameFileAfterDecompressionException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("RenameFileAfterDecompressionException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.TempDecompressed != null)
			{
				info.AddValue("RenameFileAfterDecompressionException_TempDecompressed", this.TempDecompressed, typeof(string));
			}
			if (this.TargetDecompressed != null)
			{
				info.AddValue("RenameFileAfterDecompressionException_TargetDecompressed", this.TargetDecompressed, typeof(string));
			}
			if (this.Compressed != null)
			{
				info.AddValue("RenameFileAfterDecompressionException_Compressed", this.Compressed, typeof(string));
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00054938 File Offset: 0x00052B38
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to rename file '{0}' to '{1}' after decompressing file '{2}'.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.TempDecompressed != null) ? this.TempDecompressed.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.TempDecompressed != null) ? this.TempDecompressed.MarkIfInternal() : string.Empty) : ((this.TempDecompressed != null) ? this.TempDecompressed.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.TargetDecompressed != null) ? this.TargetDecompressed.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.TargetDecompressed != null) ? this.TargetDecompressed.MarkIfInternal() : string.Empty) : ((this.TargetDecompressed != null) ? this.TargetDecompressed.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Compressed != null) ? this.Compressed.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Compressed != null) ? this.Compressed.MarkIfInternal() : string.Empty) : ((this.Compressed != null) ? this.Compressed.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00054A80 File Offset: 0x00052C80
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

		// Token: 0x06001731 RID: 5937 RVA: 0x00054AA0 File Offset: 0x00052CA0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "TempDecompressed={0}", new object[] { (this.TempDecompressed != null) ? this.TempDecompressed.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "TempDecompressed={0}", new object[] { (this.TempDecompressed != null) ? this.TempDecompressed.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "TempDecompressed={0}", new object[] { (this.TempDecompressed != null) ? this.TempDecompressed.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "TargetDecompressed={0}", new object[] { (this.TargetDecompressed != null) ? this.TargetDecompressed.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "TargetDecompressed={0}", new object[] { (this.TargetDecompressed != null) ? this.TargetDecompressed.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "TargetDecompressed={0}", new object[] { (this.TargetDecompressed != null) ? this.TargetDecompressed.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Compressed={0}", new object[] { (this.Compressed != null) ? this.Compressed.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Compressed={0}", new object[] { (this.Compressed != null) ? this.Compressed.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Compressed={0}", new object[] { (this.Compressed != null) ? this.Compressed.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00054D05 File Offset: 0x00052F05
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00054D0E File Offset: 0x00052F0E
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00054D17 File Offset: 0x00052F17
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00054D05 File Offset: 0x00052F05
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00054D20 File Offset: 0x00052F20
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

		// Token: 0x04000819 RID: 2073
		private string creationMessage;

		// Token: 0x0400081A RID: 2074
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400081B RID: 2075
		private string m_tempDecompressed;

		// Token: 0x0400081C RID: 2076
		private string m_targetDecompressed;

		// Token: 0x0400081D RID: 2077
		private string m_compressed;
	}
}
