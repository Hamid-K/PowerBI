using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000079 RID: 121
	public abstract class ScorerBindingsBase : ColumnBindingsBase
	{
		// Token: 0x06000223 RID: 547 RVA: 0x0000CB50 File Offset: 0x0000AD50
		protected ScorerBindingsBase(ISchema input, ISchemaBoundMapper mapper, string suffix, bool user, params string[] namesDerived)
			: base(input, user, ScorerBindingsBase.GetOutputNames(mapper, suffix, namesDerived))
		{
			this.Mapper = mapper;
			this.DerivedColumnCount = namesDerived.Length;
			this.Suffix = suffix ?? "";
			int num;
			uint maxMetadataKind = MetadataUtils.GetMaxMetadataKind(input, ref num, "ScoreColumnSetId", null);
			this._crtScoreSet = checked(maxMetadataKind + 1U);
			this._getScoreColumnSetId = new MetadataUtils.MetadataGetter<uint>(this.GetScoreColumnSetId);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		private static string[] GetOutputNames(ISchemaBoundMapper mapper, string suffix, string[] namesDerived)
		{
			ISchema outputSchema = mapper.OutputSchema;
			int num = namesDerived.Length + outputSchema.ColumnCount;
			string[] array = new string[num];
			int num2 = 0;
			for (int i = 0; i < namesDerived.Length; i++)
			{
				array[num2++] = namesDerived[i] + suffix;
			}
			for (int j = 0; j < outputSchema.ColumnCount; j++)
			{
				array[num2++] = outputSchema.GetColumnName(j) + suffix;
			}
			return array;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000CC34 File Offset: 0x0000AE34
		protected static KeyValuePair<RoleMappedSchema.ColumnRole, string>[] LoadBaseInfo(ModelLoadContext ctx, out string suffix)
		{
			suffix = ctx.LoadString();
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num >= 0);
			KeyValuePair<RoleMappedSchema.ColumnRole, string>[] array = new KeyValuePair<RoleMappedSchema.ColumnRole, string>[num];
			for (int i = 0; i < num; i++)
			{
				string text = ctx.LoadNonEmptyString();
				string text2 = ctx.LoadNonEmptyString();
				array[i] = RoleMappedSchema.CreatePair(text, text2);
			}
			return array;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000CCA0 File Offset: 0x0000AEA0
		protected void SaveBase(ModelSaveContext ctx)
		{
			ctx.SaveString(this.Suffix);
			KeyValuePair<RoleMappedSchema.ColumnRole, string>[] array = this.Mapper.GetInputColumnRoles().ToArray<KeyValuePair<RoleMappedSchema.ColumnRole, string>>();
			ctx.Writer.Write(array.Length);
			foreach (KeyValuePair<RoleMappedSchema.ColumnRole, string> keyValuePair in array)
			{
				ctx.SaveNonEmptyString(keyValuePair.Key.Value);
				ctx.SaveNonEmptyString(keyValuePair.Value);
			}
		}

		// Token: 0x06000227 RID: 551
		public abstract void Save(ModelSaveContext ctx);

		// Token: 0x06000228 RID: 552 RVA: 0x0000CD14 File Offset: 0x0000AF14
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this.Mapper.OutputSchema.GetColumnType(iinfo - this.DerivedColumnCount);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000CF28 File Offset: 0x0000B128
		protected override IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypesCore(int iinfo)
		{
			yield return MetadataUtils.GetPair(MetadataUtils.ScoreColumnSetIdType, "ScoreColumnSetId");
			if (iinfo >= this.DerivedColumnCount)
			{
				foreach (KeyValuePair<string, ColumnType> pair in this.Mapper.OutputSchema.GetMetadataTypes(iinfo - this.DerivedColumnCount))
				{
					yield return pair;
				}
			}
			yield break;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000CF4C File Offset: 0x0000B14C
		protected override ColumnType GetMetadataTypeCore(string kind, int iinfo)
		{
			if (kind == "ScoreColumnSetId")
			{
				return MetadataUtils.ScoreColumnSetIdType;
			}
			if (iinfo < this.DerivedColumnCount)
			{
				return null;
			}
			return this.Mapper.OutputSchema.GetMetadataTypeOrNull(kind, iinfo - this.DerivedColumnCount);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000CF88 File Offset: 0x0000B188
		protected override void GetMetadataCore<TValue>(string kind, int iinfo, ref TValue value)
		{
			if (kind != null && kind == "ScoreColumnSetId")
			{
				MetadataUtils.Marshal<uint, TValue>(this._getScoreColumnSetId, iinfo, ref value);
				return;
			}
			if (iinfo < this.DerivedColumnCount)
			{
				throw MetadataUtils.ExceptGetMetadata();
			}
			this.Mapper.OutputSchema.GetMetadata<TValue>(kind, iinfo - this.DerivedColumnCount, ref value);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000D028 File Offset: 0x0000B228
		public virtual Func<int, bool> GetActiveMapperColumns(bool[] active)
		{
			return (int col) => 0 <= col && col < this.Mapper.OutputSchema.ColumnCount && active[this.MapIinfoToCol(col + this.DerivedColumnCount)];
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D055 File Offset: 0x0000B255
		protected void GetScoreColumnSetId(int iinfo, ref uint dst)
		{
			dst = this._crtScoreSet;
		}

		// Token: 0x040000C1 RID: 193
		public readonly ISchemaBoundMapper Mapper;

		// Token: 0x040000C2 RID: 194
		public readonly string Suffix;

		// Token: 0x040000C3 RID: 195
		public readonly int DerivedColumnCount;

		// Token: 0x040000C4 RID: 196
		private readonly uint _crtScoreSet;

		// Token: 0x040000C5 RID: 197
		private readonly MetadataUtils.MetadataGetter<uint> _getScoreColumnSetId;
	}
}
