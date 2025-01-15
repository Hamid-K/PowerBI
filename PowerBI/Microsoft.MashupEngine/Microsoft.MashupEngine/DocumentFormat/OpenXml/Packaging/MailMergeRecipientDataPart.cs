using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002168 RID: 8552
	internal class MailMergeRecipientDataPart : OpenXmlPart
	{
		// Token: 0x0600D5EE RID: 54766 RVA: 0x002A6C48 File Offset: 0x002A4E48
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (MailMergeRecipientDataPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				MailMergeRecipientDataPart._partConstraint = dictionary;
			}
			return MailMergeRecipientDataPart._partConstraint;
		}

		// Token: 0x0600D5EF RID: 54767 RVA: 0x002A6C70 File Offset: 0x002A4E70
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (MailMergeRecipientDataPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				MailMergeRecipientDataPart._dataPartReferenceConstraint = dictionary;
			}
			return MailMergeRecipientDataPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D5F0 RID: 54768 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal MailMergeRecipientDataPart()
		{
		}

		// Token: 0x17003431 RID: 13361
		// (get) Token: 0x0600D5F1 RID: 54769 RVA: 0x002A6C95 File Offset: 0x002A4E95
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/recipientData";
			}
		}

		// Token: 0x17003432 RID: 13362
		// (get) Token: 0x0600D5F2 RID: 54770 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003433 RID: 13363
		// (get) Token: 0x0600D5F3 RID: 54771 RVA: 0x002A6C9C File Offset: 0x002A4E9C
		internal sealed override string TargetName
		{
			get
			{
				return "recipients";
			}
		}

		// Token: 0x17003434 RID: 13364
		// (get) Token: 0x0600D5F4 RID: 54772 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003435 RID: 13365
		// (get) Token: 0x0600D5F5 RID: 54773 RVA: 0x002A6CA3 File Offset: 0x002A4EA3
		// (set) Token: 0x0600D5F6 RID: 54774 RVA: 0x002A6CAB File Offset: 0x002A4EAB
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value;
			}
		}

		// Token: 0x17003436 RID: 13366
		// (get) Token: 0x0600D5F7 RID: 54775 RVA: 0x002A6CB4 File Offset: 0x002A4EB4
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				if (this.Recipients != null)
				{
					return this.Recipients;
				}
				return this.MailMergeRecipients;
			}
		}

		// Token: 0x17003437 RID: 13367
		// (get) Token: 0x0600D5F8 RID: 54776 RVA: 0x002A6CCB File Offset: 0x002A4ECB
		// (set) Token: 0x0600D5F9 RID: 54777 RVA: 0x002A6CE0 File Offset: 0x002A4EE0
		public Recipients Recipients
		{
			get
			{
				this.TryLoadRootElement();
				return this._rootEle as Recipients;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.MailMergeRecipients != null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.PropertyMutualExclusive, new object[] { "Recipients", "MailMergeRecipients" }));
				}
				base.SetDomTree(value);
			}
		}

		// Token: 0x17003438 RID: 13368
		// (get) Token: 0x0600D5FA RID: 54778 RVA: 0x002A6D37 File Offset: 0x002A4F37
		// (set) Token: 0x0600D5FB RID: 54779 RVA: 0x002A6D4C File Offset: 0x002A4F4C
		public MailMergeRecipients MailMergeRecipients
		{
			get
			{
				this.TryLoadRootElement();
				return this._rootEle as MailMergeRecipients;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.Recipients != null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionMessages.PropertyMutualExclusive, new object[] { "MailMergeRecipients", "Recipients" }));
				}
				base.SetDomTree(value);
			}
		}

		// Token: 0x0600D5FC RID: 54780 RVA: 0x002A6DA4 File Offset: 0x002A4FA4
		private void TryLoadRootElement()
		{
			if (this._rootEle == null)
			{
				try
				{
					base.LoadDomTree<Recipients>();
				}
				catch (InvalidDataException)
				{
				}
				if (this._rootEle == null)
				{
					base.LoadDomTree<MailMergeRecipients>();
				}
			}
		}

		// Token: 0x04006A45 RID: 27205
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/recipientData";

		// Token: 0x04006A46 RID: 27206
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A47 RID: 27207
		internal const string TargetNameConstant = "recipients";

		// Token: 0x04006A48 RID: 27208
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A49 RID: 27209
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A4A RID: 27210
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private OpenXmlPartRootElement _rootEle;
	}
}
