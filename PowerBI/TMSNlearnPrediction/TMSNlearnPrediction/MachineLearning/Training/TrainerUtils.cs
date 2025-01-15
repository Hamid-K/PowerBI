using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Training
{
	// Token: 0x02000489 RID: 1161
	public static class TrainerUtils
	{
		// Token: 0x0600182F RID: 6191 RVA: 0x0008A7A0 File Offset: 0x000889A0
		public static void CheckFeatureFloatVector(this RoleMappedData data)
		{
			Contracts.CheckValue<RoleMappedData>(data, "data");
			ColumnInfo feature = data.Schema.Feature;
			if (feature == null)
			{
				throw Contracts.ExceptParam("data", "Training data must specify a feature column.");
			}
			if (!feature.Type.IsKnownSizeVector || feature.Type.ItemType != NumberType.Float)
			{
				throw Contracts.ExceptParam("data", "Training feature column '{0}' must be a known-size vector of R4, but has type: {1}.", new object[] { feature.Name, feature.Type });
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0008A820 File Offset: 0x00088A20
		public static void CheckFeatureFloatVector(this RoleMappedData data, out int length)
		{
			data.CheckFeatureFloatVector();
			length = data.Schema.Feature.Type.VectorSize;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0008A840 File Offset: 0x00088A40
		public static void CheckBinaryLabel(this RoleMappedData data)
		{
			Contracts.CheckValue<RoleMappedData>(data, "data");
			ColumnInfo label = data.Schema.Label;
			if (label == null)
			{
				throw Contracts.ExceptParam("data", "Training data must specify a label column.");
			}
			if (!label.Type.IsBool && label.Type != NumberType.R4 && label.Type != NumberType.R8 && label.Type.KeyCount != 2)
			{
				if (label.Type.IsKey)
				{
					if (label.Type.KeyCount == 1)
					{
						throw Contracts.ExceptParam("data", "The label column '{0}' of the training data has only one class. Two classes are required for binary classification.", new object[] { label.Name });
					}
					if (label.Type.KeyCount > 2)
					{
						throw Contracts.ExceptParam("data", "The label column '{0}' of the training data has more than two classes. Only two classes are allowed for binary classification.", new object[] { label.Name });
					}
				}
				throw Contracts.ExceptParam("data", "The label column '{0}' of the training data has a data type not suitable for binary classification: {1}. Type must be Bool, R4, R8 or Key with two classes.", new object[] { label.Name, label.Type });
			}
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0008A950 File Offset: 0x00088B50
		public static void CheckRegressionLabel(this RoleMappedData data)
		{
			Contracts.CheckValue<RoleMappedData>(data, "data");
			ColumnInfo label = data.Schema.Label;
			if (label == null)
			{
				throw Contracts.ExceptParam("data", "Training data must specify a label column.");
			}
			if (label.Type != NumberType.R4 && label.Type != NumberType.R8)
			{
				throw Contracts.ExceptParam("data", "Training label column '{0}' type isn't suitable for regression: {1}. Type must be R4 or R8.", new object[] { label.Name, label.Type });
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0008A9CC File Offset: 0x00088BCC
		public static void CheckMultiClassLabel(this RoleMappedData data, out int count)
		{
			Contracts.CheckValue<RoleMappedData>(data, "data");
			ColumnInfo label = data.Schema.Label;
			if (label == null)
			{
				throw Contracts.ExceptParam("data", "Training data must specify a label column.");
			}
			if (label.Type.KeyCount > 0)
			{
				count = label.Type.KeyCount;
				return;
			}
			if (label.Type != NumberType.R4 && label.Type != NumberType.R8)
			{
				throw Contracts.ExceptParam("data", "Training label column '{0}' type is not valid for multi-class: {1}. Type must be R4 or R8.", new object[] { label.Name, label.Type });
			}
			int num = -1;
			using (FloatLabelCursor floatLabelCursor = new FloatLabelCursor(data, CursOpt.Label, null, new int[0]))
			{
				while (floatLabelCursor.MoveNext())
				{
					int num2 = (int)floatLabelCursor.Label;
					if ((float)num2 != floatLabelCursor.Label || num2 < 0)
					{
						throw Contracts.ExceptParam("data", "Training label column '{0}' contains invalid values for multi-class: {1}.", new object[] { label.Name, floatLabelCursor.Label });
					}
					if (num < num2)
					{
						num = num2;
					}
				}
			}
			if (num < 0)
			{
				throw Contracts.ExceptParam("data", "Training label column '{0}' contains no valid values for multi-class.", new object[] { label.Name });
			}
			if (num >= 2146435071)
			{
				throw Contracts.ExceptParam("data", "Maximum label is too large for multi-class: {0}.", new object[] { num });
			}
			count = num + 1;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0008AB48 File Offset: 0x00088D48
		public static void CheckMultiOutputRegressionLabel(this RoleMappedData data)
		{
			Contracts.CheckValue<RoleMappedData>(data, "data");
			ColumnInfo label = data.Schema.Label;
			if (label == null)
			{
				throw Contracts.ExceptParam("data", "Training data must specify a label column.");
			}
			if (!label.Type.IsKnownSizeVector || label.Type.ItemType != NumberType.Float)
			{
				throw Contracts.ExceptParam("data", "Training label column '{0}' must be a known-size vector of R4, but has type: {1}.", new object[] { label.Name, label.Type });
			}
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0008ABC8 File Offset: 0x00088DC8
		public static void CheckOptFloatWeight(this RoleMappedData data)
		{
			Contracts.CheckValue<RoleMappedData>(data, "data");
			ColumnInfo weight = data.Schema.Weight;
			if (weight == null)
			{
				return;
			}
			if (weight.Type != NumberType.R4 && weight.Type != NumberType.R8)
			{
				throw Contracts.ExceptParam("data", "Training weight column '{0}' must be of floating point numeric type, but has type: {1}.", new object[] { weight.Name, weight.Type });
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0008AC34 File Offset: 0x00088E34
		public static void CheckOptGroup(this RoleMappedData data)
		{
			Contracts.CheckValue<RoleMappedData>(data, "data");
			ColumnInfo group = data.Schema.Group;
			if (group == null)
			{
				return;
			}
			if (group.Type.IsKey)
			{
				return;
			}
			throw Contracts.ExceptParam("data", "Training group column '{0}' type is invalid: {1}. Must be Key type.", new object[] { group.Name, group.Type });
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0008AC94 File Offset: 0x00088E94
		private static Func<int, bool> CreatePredicate(RoleMappedData data, CursOpt opt, IEnumerable<int> extraCols)
		{
			HashSet<int> hashSet = new HashSet<int>();
			if ((opt & CursOpt.Label) != (CursOpt)0U)
			{
				TrainerUtils.AddOpt(hashSet, data.Schema.Label);
			}
			if ((opt & CursOpt.Features) != (CursOpt)0U)
			{
				TrainerUtils.AddOpt(hashSet, data.Schema.Feature);
			}
			if ((opt & CursOpt.Weight) != (CursOpt)0U)
			{
				TrainerUtils.AddOpt(hashSet, data.Schema.Weight);
			}
			if ((opt & CursOpt.Group) != (CursOpt)0U)
			{
				TrainerUtils.AddOpt(hashSet, data.Schema.Group);
			}
			if (extraCols != null)
			{
				foreach (int num in extraCols)
				{
					hashSet.Add(num);
				}
			}
			return new Func<int, bool>(hashSet.Contains);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0008AD4C File Offset: 0x00088F4C
		public static IRowCursor CreateRowCursor(this RoleMappedData data, CursOpt opt, IRandom rand, IEnumerable<int> extraCols = null)
		{
			return data.Data.GetRowCursor(TrainerUtils.CreatePredicate(data, opt, extraCols), rand);
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0008AD62 File Offset: 0x00088F62
		public static IRowCursor[] CreateRowCursorSet(this RoleMappedData data, out IRowCursorConsolidator consolidator, CursOpt opt, int n, IRandom rand, IEnumerable<int> extraCols = null)
		{
			return data.Data.GetRowCursorSet(ref consolidator, TrainerUtils.CreatePredicate(data, opt, extraCols), n, rand);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0008AD7C File Offset: 0x00088F7C
		private static void AddOpt(HashSet<int> cols, ColumnInfo info)
		{
			if (info != null)
			{
				cols.Add(info.Index);
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0008AD90 File Offset: 0x00088F90
		public static ValueGetter<VBuffer<float>> GetFeatureFloatVectorGetter(this IRow row, RoleMappedSchema schema)
		{
			Contracts.CheckValue<IRow>(row, "row");
			Contracts.CheckValue<RoleMappedSchema>(schema, "schema");
			Contracts.CheckParam(schema.Schema == row.Schema, "schema", "schemas don't match!");
			Contracts.CheckParam(schema.Feature != null, "schema", "Missing feature column");
			return row.GetGetter<VBuffer<float>>(schema.Feature.Index);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0008ADFC File Offset: 0x00088FFC
		public static ValueGetter<VBuffer<float>> GetFeatureFloatVectorGetter(this IRow row, RoleMappedData data)
		{
			Contracts.CheckValue<RoleMappedData>(data, "data");
			return row.GetFeatureFloatVectorGetter(data.Schema);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0008AE18 File Offset: 0x00089018
		public static ValueGetter<float> GetLabelFloatGetter(this IRow row, RoleMappedSchema schema)
		{
			Contracts.CheckValue<IRow>(row, "row");
			Contracts.CheckValue<RoleMappedSchema>(schema, "schema");
			Contracts.CheckParam(schema.Schema == row.Schema, "schema", "schemas don't match!");
			Contracts.CheckParam(schema.Label != null, "schema", "Missing label column");
			return RowCursorUtils.GetLabelGetter(row, schema.Label.Index);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0008AE84 File Offset: 0x00089084
		public static ValueGetter<float> GetLabelFloatGetter(this IRow row, RoleMappedData data)
		{
			Contracts.CheckValue<RoleMappedData>(data, "data");
			return row.GetLabelFloatGetter(data.Schema);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0008AEA0 File Offset: 0x000890A0
		public static ValueGetter<float> GetOptWeightFloatGetter(this IRow row, RoleMappedSchema schema)
		{
			Contracts.CheckValue<IRow>(row, "row");
			Contracts.CheckValue<RoleMappedSchema>(schema, "schema");
			Contracts.Check(schema.Schema == row.Schema, "schemas don't match!");
			ColumnInfo weight = schema.Weight;
			if (weight == null)
			{
				return null;
			}
			return RowCursorUtils.GetGetterAs<float>(NumberType.Float, row, weight.Index);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0008AEF8 File Offset: 0x000890F8
		public static ValueGetter<float> GetOptWeightFloatGetter(this IRow row, RoleMappedData data)
		{
			Contracts.CheckValue<RoleMappedData>(data, "data");
			return row.GetOptWeightFloatGetter(data.Schema);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0008AF14 File Offset: 0x00089114
		public static ValueGetter<ulong> GetOptGroupGetter(this IRow row, RoleMappedSchema schema)
		{
			Contracts.CheckValue<IRow>(row, "row");
			Contracts.CheckValue<RoleMappedSchema>(schema, "schema");
			Contracts.Check(schema.Schema == row.Schema, "schemas don't match!");
			ColumnInfo group = schema.Group;
			if (group == null)
			{
				return null;
			}
			return RowCursorUtils.GetGetterAs<ulong>(NumberType.U8, row, group.Index);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0008AF6C File Offset: 0x0008916C
		public static ValueGetter<ulong> GetOptGroupGetter(this IRow row, RoleMappedData data)
		{
			Contracts.CheckValue<RoleMappedData>(data, "data");
			return row.GetOptGroupGetter(data.Schema);
		}
	}
}
