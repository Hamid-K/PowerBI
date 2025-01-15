using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200214E RID: 8526
	internal class ExtendedPart : OpenXmlPart
	{
		// Token: 0x0600D3DC RID: 54236 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected ExtendedPart()
		{
		}

		// Token: 0x0600D3DD RID: 54237 RVA: 0x002A1ACD File Offset: 0x0029FCCD
		protected internal ExtendedPart(string relationshipType)
		{
			this._relationshipType = relationshipType;
		}

		// Token: 0x17003322 RID: 13090
		// (get) Token: 0x0600D3DE RID: 54238 RVA: 0x002A1ADC File Offset: 0x0029FCDC
		public override string RelationshipType
		{
			get
			{
				return this._relationshipType;
			}
		}

		// Token: 0x17003323 RID: 13091
		// (get) Token: 0x0600D3DF RID: 54239 RVA: 0x002A1AE4 File Offset: 0x0029FCE4
		internal override string TargetFileExtension
		{
			get
			{
				return ".dat";
			}
		}

		// Token: 0x17003324 RID: 13092
		// (get) Token: 0x0600D3E0 RID: 54240 RVA: 0x002A1AEB File Offset: 0x0029FCEB
		internal override string TargetPath
		{
			get
			{
				return "udata";
			}
		}

		// Token: 0x17003325 RID: 13093
		// (get) Token: 0x0600D3E1 RID: 54241 RVA: 0x002958E1 File Offset: 0x00293AE1
		internal override string TargetName
		{
			get
			{
				return "data";
			}
		}

		// Token: 0x17003326 RID: 13094
		// (get) Token: 0x0600D3E2 RID: 54242 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600D3E3 RID: 54243 RVA: 0x002A1AF2 File Offset: 0x0029FCF2
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			this.ThrowIfObjectDisposed();
			return ExtendedPart._partConstraints;
		}

		// Token: 0x0600D3E4 RID: 54244 RVA: 0x002A1AFF File Offset: 0x0029FCFF
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			this.ThrowIfObjectDisposed();
			return ExtendedPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D3E5 RID: 54245 RVA: 0x00002105 File Offset: 0x00000305
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return false;
		}

		// Token: 0x0600D3E6 RID: 54246 RVA: 0x002A1B0C File Offset: 0x0029FD0C
		internal override OpenXmlPart AddPartFrom(OpenXmlPart subPart, string rId)
		{
			this.ThrowIfObjectDisposed();
			if (subPart == null)
			{
				throw new ArgumentNullException("subPart");
			}
			if (subPart.OpenXmlPackage != this.InternalOpenXmlPackage || !base.IsChildPart(subPart))
			{
				return base.AddSubPart(subPart, rId);
			}
			if (rId != null && rId != base.GetIdOfPart(subPart))
			{
				throw new InvalidOperationException(ExceptionMessages.PartExistsWithDifferentRelationshipId);
			}
			return subPart;
		}

		// Token: 0x0600D3E7 RID: 54247 RVA: 0x002A1B6C File Offset: 0x0029FD6C
		internal override void InitPart<T>(T newPart, string contentType, string id)
		{
			this.ThrowIfObjectDisposed();
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (contentType == string.Empty)
			{
				throw new ArgumentException(ExceptionMessages.StringArgumentEmptyException, "contentType");
			}
			newPart.CreateInternal(this.InternalOpenXmlPackage, this.ThisOpenXmlPart, contentType, null);
			string text = base.AttachChild(newPart, id);
			base.ChildrenParts.Add(text, newPart);
		}

		// Token: 0x040069B3 RID: 27059
		private const string DefaultTargetExt = ".dat";

		// Token: 0x040069B4 RID: 27060
		private string _relationshipType;

		// Token: 0x040069B5 RID: 27061
		private static Dictionary<string, PartConstraintRule> _partConstraints = new Dictionary<string, PartConstraintRule>();

		// Token: 0x040069B6 RID: 27062
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint = new Dictionary<string, PartConstraintRule>();
	}
}
