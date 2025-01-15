using System;
using System.Collections.Generic;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200021E RID: 542
	public abstract class ScoreMapperSchemaBase : ISchema
	{
		// Token: 0x06000C20 RID: 3104 RVA: 0x00042550 File Offset: 0x00040750
		public ScoreMapperSchemaBase(ColumnType scoreType, string scoreColumnKind)
		{
			Contracts.CheckValue<ColumnType>(scoreType, "scoreType");
			Contracts.CheckNonEmpty(scoreColumnKind, "scoreColumnKind");
			this._scoreType = scoreType;
			this._scoreColumnKind = scoreColumnKind;
			this._getScoreValueKind = new MetadataUtils.MetadataGetter<DvText>(this.GetScoreValueKind);
			this._getScoreColumnKind = new MetadataUtils.MetadataGetter<DvText>(this.GetScoreColumnKind);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000C21 RID: 3105
		public abstract int ColumnCount { get; }

		// Token: 0x06000C22 RID: 3106 RVA: 0x000425B0 File Offset: 0x000407B0
		private void CheckColZero(int col, string methName)
		{
			if (col == 0)
			{
				return;
			}
			throw Contracts.Except("Derived class should have overriden {0} to handle all columns except zero", new object[] { methName });
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000425D7 File Offset: 0x000407D7
		public virtual ColumnType GetColumnType(int col)
		{
			Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
			this.CheckColZero(col, "GetColumnType");
			return this._scoreType;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00042605 File Offset: 0x00040805
		public virtual bool TryGetColumnIndex(string name, out int col)
		{
			col = 0;
			return name == "Score";
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00042615 File Offset: 0x00040815
		public virtual string GetColumnName(int col)
		{
			Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
			this.CheckColZero(col, "GetColumnName");
			return "Score";
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00042644 File Offset: 0x00040844
		public virtual IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
		{
			Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
			return new KeyValuePair<string, ColumnType>[]
			{
				MetadataUtils.GetPair(TextType.Instance, "ScoreColumnKind"),
				MetadataUtils.GetPair(TextType.Instance, "ScoreValueKind")
			};
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000426AC File Offset: 0x000408AC
		public virtual ColumnType GetMetadataTypeOrNull(string kind, int col)
		{
			Contracts.CheckNonEmpty(kind, "kind");
			Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
			if (kind != null && (kind == "ScoreColumnKind" || kind == "ScoreValueKind"))
			{
				return TextType.Instance;
			}
			return null;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00042708 File Offset: 0x00040908
		public virtual void GetMetadata<TValue>(string kind, int col, ref TValue value)
		{
			Contracts.CheckNonEmpty(kind, "kind");
			Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
			if (kind != null)
			{
				if (kind == "ScoreColumnKind")
				{
					MetadataUtils.Marshal<DvText, TValue>(this._getScoreColumnKind, col, ref value);
					return;
				}
				if (kind == "ScoreValueKind")
				{
					MetadataUtils.Marshal<DvText, TValue>(this._getScoreValueKind, col, ref value);
					return;
				}
			}
			throw MetadataUtils.ExceptGetMetadata();
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0004277D File Offset: 0x0004097D
		protected virtual void GetScoreValueKind(int col, ref DvText dst)
		{
			this.CheckColZero(col, "GetScoreValueKind");
			dst = new DvText("Score");
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0004279B File Offset: 0x0004099B
		private void GetScoreColumnKind(int col, ref DvText dst)
		{
			dst = new DvText(this._scoreColumnKind);
		}

		// Token: 0x040006AE RID: 1710
		protected readonly ColumnType _scoreType;

		// Token: 0x040006AF RID: 1711
		protected readonly string _scoreColumnKind;

		// Token: 0x040006B0 RID: 1712
		protected readonly MetadataUtils.MetadataGetter<DvText> _getScoreValueKind;

		// Token: 0x040006B1 RID: 1713
		protected readonly MetadataUtils.MetadataGetter<DvText> _getScoreColumnKind;
	}
}
