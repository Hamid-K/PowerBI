using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000082 RID: 130
	[XmlRoot(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
	[Editor("Microsoft.AnalysisServices.Design.ErrorConfigurationPropertyTypeEditor, Microsoft.AnalysisServices.Design.AS", typeof(UITypeEditor))]
	[TypeConverter("Microsoft.AnalysisServices.Design.ErrorConfigurationTypeConverter, Microsoft.AnalysisServices.Design.AS")]
	[Guid("DFC2EFB6-E3D4-4A77-9776-932EA64DA506")]
	public sealed class ErrorConfiguration : Component
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x0002400A File Offset: 0x0002220A
		public ErrorConfiguration()
		{
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00024020 File Offset: 0x00022220
		public ErrorConfiguration(string keyErrorLogFile)
		{
			this.KeyErrorLogFile = keyErrorLogFile;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0002403D File Offset: 0x0002223D
		public ErrorConfiguration(string keyErrorLogFile, long keyErrorLimit)
		{
			this.KeyErrorLogFile = keyErrorLogFile;
			this.KeyErrorLimit = keyErrorLimit;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00024061 File Offset: 0x00022261
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x00024069 File Offset: 0x00022269
		[DefaultValue(0L)]
		[NotifyParentProperty(true)]
		[LocalizedDescription("PropertyDescription_ErrorConfiguration_KeyErrorLimit")]
		public long KeyErrorLimit
		{
			get
			{
				return this.keyErrorLimit;
			}
			set
			{
				this.keyErrorLimit = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00024072 File Offset: 0x00022272
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x0002409A File Offset: 0x0002229A
		[XmlElement(IsNullable = false)]
		[DefaultValue(null)]
		[TypeConverter("Microsoft.AnalysisServices.Design.LogFileUITypeConverter, Microsoft.AnalysisServices.Design.AS")]
		[Editor("Microsoft.AnalysisServices.Design.LogFileUITypeEditor, Microsoft.AnalysisServices.Design.AS", typeof(UITypeEditor))]
		[LocalizedDescription("PropertyDescription_ErrorConfiguration_KeyErrorLogFile")]
		public string KeyErrorLogFile
		{
			get
			{
				if (this.parent != null)
				{
					return (string)this.parent.GetErrorConfigurationValue(this, this.keyErrorLogFile);
				}
				return this.keyErrorLogFile;
			}
			set
			{
				Utils.CheckValidPath(value);
				if (this.parent != null)
				{
					this.parent.SetErrorConfigurationValue(this, value);
				}
				this.keyErrorLogFile = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x000240BE File Offset: 0x000222BE
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x000240C6 File Offset: 0x000222C6
		[DefaultValue(KeyErrorAction.ConvertToUnknown)]
		[NotifyParentProperty(true)]
		[LocalizedDescription("PropertyDescription_ErrorConfiguration_KeyErrorAction")]
		public KeyErrorAction KeyErrorAction
		{
			get
			{
				return this.keyErrorAction;
			}
			set
			{
				this.keyErrorAction = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x000240CF File Offset: 0x000222CF
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x000240D7 File Offset: 0x000222D7
		[DefaultValue(KeyErrorLimitAction.StopProcessing)]
		[NotifyParentProperty(true)]
		[LocalizedDescription("PropertyDescription_ErrorConfiguration_KeyErrorLimitAction")]
		public KeyErrorLimitAction KeyErrorLimitAction
		{
			get
			{
				return this.keyErrorLimitAction;
			}
			set
			{
				this.keyErrorLimitAction = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x000240E0 File Offset: 0x000222E0
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x000240E8 File Offset: 0x000222E8
		[DefaultValue(ErrorOption.ReportAndContinue)]
		[NotifyParentProperty(true)]
		[LocalizedDescription("PropertyDescription_ErrorConfiguration_KeyNotFound")]
		public ErrorOption KeyNotFound
		{
			get
			{
				return this.keyNotFound;
			}
			set
			{
				this.keyNotFound = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x000240F1 File Offset: 0x000222F1
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x000240F9 File Offset: 0x000222F9
		[DefaultValue(ErrorOption.IgnoreError)]
		[NotifyParentProperty(true)]
		[LocalizedDescription("PropertyDescription_ErrorConfiguration_KeyDuplicate")]
		public ErrorOption KeyDuplicate
		{
			get
			{
				return this.keyDuplicate;
			}
			set
			{
				this.keyDuplicate = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00024102 File Offset: 0x00022302
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0002410A File Offset: 0x0002230A
		[DefaultValue(ErrorOption.IgnoreError)]
		[NotifyParentProperty(true)]
		[LocalizedDescription("PropertyDescription_ErrorConfiguration_NullKeyConvertedToUnknown")]
		public ErrorOption NullKeyConvertedToUnknown
		{
			get
			{
				return this.nullKeyConvertedToUnknown;
			}
			set
			{
				this.nullKeyConvertedToUnknown = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00024113 File Offset: 0x00022313
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0002411B File Offset: 0x0002231B
		[DefaultValue(ErrorOption.ReportAndContinue)]
		[NotifyParentProperty(true)]
		[LocalizedDescription("PropertyDescription_ErrorConfiguration_NullKeyNotAllowed")]
		public ErrorOption NullKeyNotAllowed
		{
			get
			{
				return this.nullKeyNotAllowed;
			}
			set
			{
				this.nullKeyNotAllowed = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00024124 File Offset: 0x00022324
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0002412C File Offset: 0x0002232C
		[DefaultValue(ErrorOption.IgnoreError)]
		[NotifyParentProperty(true)]
		[LocalizedDescription("PropertyDescription_ErrorConfiguration_CalculationError")]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2010/engine/200")]
		public ErrorOption CalculationError
		{
			get
			{
				return this.calculationError;
			}
			set
			{
				this.calculationError = value;
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00024138 File Offset: 0x00022338
		public ErrorConfiguration CopyTo(ErrorConfiguration obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			obj.KeyErrorLimit = this.KeyErrorLimit;
			obj.KeyErrorLogFile = this.keyErrorLogFile;
			obj.KeyErrorAction = this.KeyErrorAction;
			obj.KeyErrorLimitAction = this.KeyErrorLimitAction;
			obj.KeyNotFound = this.KeyNotFound;
			obj.KeyDuplicate = this.KeyDuplicate;
			obj.NullKeyConvertedToUnknown = this.NullKeyConvertedToUnknown;
			obj.NullKeyNotAllowed = this.NullKeyNotAllowed;
			obj.CalculationError = this.CalculationError;
			return obj;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x000241C0 File Offset: 0x000223C0
		public ErrorConfiguration Clone()
		{
			return this.CopyTo(new ErrorConfiguration());
		}

		// Token: 0x04000446 RID: 1094
		internal MajorObject parent;

		// Token: 0x04000447 RID: 1095
		private long keyErrorLimit;

		// Token: 0x04000448 RID: 1096
		private string keyErrorLogFile;

		// Token: 0x04000449 RID: 1097
		private KeyErrorAction keyErrorAction;

		// Token: 0x0400044A RID: 1098
		private KeyErrorLimitAction keyErrorLimitAction;

		// Token: 0x0400044B RID: 1099
		private ErrorOption keyNotFound = ErrorOption.ReportAndContinue;

		// Token: 0x0400044C RID: 1100
		private ErrorOption keyDuplicate;

		// Token: 0x0400044D RID: 1101
		private ErrorOption nullKeyConvertedToUnknown;

		// Token: 0x0400044E RID: 1102
		private ErrorOption nullKeyNotAllowed = ErrorOption.ReportAndContinue;

		// Token: 0x0400044F RID: 1103
		private ErrorOption calculationError;
	}
}
