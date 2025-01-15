using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office.Word;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021AB RID: 8619
	internal class VbaDataPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DAAA RID: 55978 RVA: 0x002ADE84 File Offset: 0x002AC084
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (VbaDataPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				VbaDataPart._partConstraint = dictionary;
			}
			return VbaDataPart._partConstraint;
		}

		// Token: 0x0600DAAB RID: 55979 RVA: 0x002ADEAC File Offset: 0x002AC0AC
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (VbaDataPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				VbaDataPart._dataPartReferenceConstraint = dictionary;
			}
			return VbaDataPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DAAC RID: 55980 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal VbaDataPart()
		{
		}

		// Token: 0x170036D4 RID: 14036
		// (get) Token: 0x0600DAAD RID: 55981 RVA: 0x002ADED1 File Offset: 0x002AC0D1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/wordVbaData";
			}
		}

		// Token: 0x170036D5 RID: 14037
		// (get) Token: 0x0600DAAE RID: 55982 RVA: 0x002ADED8 File Offset: 0x002AC0D8
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-word.vbaData+xml";
			}
		}

		// Token: 0x170036D6 RID: 14038
		// (get) Token: 0x0600DAAF RID: 55983 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x170036D7 RID: 14039
		// (get) Token: 0x0600DAB0 RID: 55984 RVA: 0x002ADEDF File Offset: 0x002AC0DF
		internal sealed override string TargetName
		{
			get
			{
				return "vbaData";
			}
		}

		// Token: 0x170036D8 RID: 14040
		// (get) Token: 0x0600DAB1 RID: 55985 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170036D9 RID: 14041
		// (get) Token: 0x0600DAB2 RID: 55986 RVA: 0x002ADEE6 File Offset: 0x002AC0E6
		// (set) Token: 0x0600DAB3 RID: 55987 RVA: 0x002ADEEE File Offset: 0x002AC0EE
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as VbaSuppData;
			}
		}

		// Token: 0x170036DA RID: 14042
		// (get) Token: 0x0600DAB4 RID: 55988 RVA: 0x002ADEFC File Offset: 0x002AC0FC
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.VbaSuppData;
			}
		}

		// Token: 0x170036DB RID: 14043
		// (get) Token: 0x0600DAB5 RID: 55989 RVA: 0x002ADF04 File Offset: 0x002AC104
		// (set) Token: 0x0600DAB6 RID: 55990 RVA: 0x002A3296 File Offset: 0x002A1496
		public VbaSuppData VbaSuppData
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<VbaSuppData>();
				}
				return this._rootEle;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetDomTree(value);
			}
		}

		// Token: 0x04006C05 RID: 27653
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/wordVbaData";

		// Token: 0x04006C06 RID: 27654
		internal const string ContentTypeConstant = "application/vnd.ms-word.vbaData+xml";

		// Token: 0x04006C07 RID: 27655
		internal const string TargetPathConstant = ".";

		// Token: 0x04006C08 RID: 27656
		internal const string TargetNameConstant = "vbaData";

		// Token: 0x04006C09 RID: 27657
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C0A RID: 27658
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006C0B RID: 27659
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VbaSuppData _rootEle;
	}
}
