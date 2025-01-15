using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x0200002F RID: 47
	internal abstract class SqlLiteralExpression : SqlExpression
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00008148 File Offset: 0x00006348
		protected SqlLiteralExpression(IQPExpressionInfo qpInfo, SqlBatch sqlBatch)
			: base(SqlLiteralExpression.ComputeIsNullable(qpInfo))
		{
			if (qpInfo == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("qpInfo"));
			}
			if (sqlBatch == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("sqlBatch"));
			}
			this.m_literalNode = qpInfo.Expression.NodeAsLiteral;
			if (this.m_literalNode == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Specified qpInfo must be a literal value.", Array.Empty<object>());
			}
			this.m_entityKeyTarget = qpInfo.Expression.GetResultType().EntityKeyTarget;
			this.m_sqlBatch = sqlBatch;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000081D0 File Offset: 0x000063D0
		internal ISqlSnippet CreateSqlSnippetForBoolean(bool boolean)
		{
			if (!boolean)
			{
				return this.m_sqlBatch.SqlBitFalseSnippet;
			}
			return this.m_sqlBatch.SqlBitTrueSnippet;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000081EC File Offset: 0x000063EC
		internal SqlLiteralExpression.LiteralSet ToSet()
		{
			if (base.Values.Count == 1 && base.Values[0] is SqlLiteralExpression.LiteralSet)
			{
				return (SqlLiteralExpression.LiteralSet)base.Values[0];
			}
			SqlLiteralExpression.LiteralSet literalSet = new SqlLiteralExpression.LiteralSet();
			SqlTupleExpression sqlTupleExpression = new SqlTupleExpression();
			foreach (ISqlSnippet sqlSnippet in base.Values)
			{
				SqlExpression sqlExpression = sqlSnippet as SqlExpression;
				sqlTupleExpression.AddTupleValue(sqlSnippet, (sqlExpression != null) ? sqlExpression.IsNullable : this.IsNullable);
			}
			literalSet.Tuples.Add(sqlTupleExpression);
			return literalSet;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00004555 File Offset: 0x00002755
		internal override bool CanGroupBy
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000082A8 File Offset: 0x000064A8
		protected override void InitValues()
		{
			switch (this.m_literalNode.DataType)
			{
			case DataType.String:
				base.Values.Add(this.CreateSqlSnippetForString());
				return;
			case DataType.Integer:
				base.Values.Add(this.CreateSqlSnippetForInteger());
				return;
			case DataType.Decimal:
				base.Values.Add(this.CreateSqlSnippetForDecimal());
				return;
			case DataType.Float:
				base.Values.Add(this.CreateSqlSnippetForFloat());
				return;
			case DataType.Boolean:
				base.Values.Add(this.CreateSqlSnippetForBoolean());
				return;
			case DataType.DateTime:
				base.Values.Add(this.CreateSqlSnippetForDateTime());
				return;
			case DataType.Binary:
				throw SQEAssert.AssertFalseAndThrow("Binary literals are not supported.", Array.Empty<object>());
			case DataType.EntityKey:
				this.CreateSqlSnippetsForEntityKey();
				return;
			case DataType.Null:
				throw SQEAssert.AssertFalseAndThrow("Null literals are not supported.", Array.Empty<object>());
			case DataType.Time:
				base.Values.Add(this.CreateSqlSnippetForTime());
				return;
			default:
				throw SQEAssert.AssertFalseAndThrow("Unknown literal type: {0}.", new object[] { this.m_literalNode.DataType });
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000083BC File Offset: 0x000065BC
		protected SqlBatch SqlBatch
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_sqlBatch;
			}
		}

		// Token: 0x060001C1 RID: 449
		protected abstract ISqlSnippet CreateBasicSqlSnippetForString(string str);

		// Token: 0x060001C2 RID: 450
		protected abstract ISqlSnippet CreateBasicSqlSnippetForDateTime(DateTime dateTime);

		// Token: 0x060001C3 RID: 451
		protected abstract ISqlSnippet CreateBasicSqlSnippetForTime(TimeSpan time);

		// Token: 0x060001C4 RID: 452 RVA: 0x000083C4 File Offset: 0x000065C4
		private ISqlSnippet CreateSqlSnippetForString()
		{
			if (this.m_literalNode.Cardinality == Cardinality.One)
			{
				return this.CreateBasicSqlSnippetForString(this.m_literalNode.ValueAsString);
			}
			SqlLiteralExpression.LiteralSet literalSet = new SqlLiteralExpression.LiteralSet();
			foreach (string text in this.m_literalNode.ValueAsStringSet)
			{
				literalSet.Tuples.Add(this.CreateBasicSqlSnippetForString(text));
			}
			return literalSet;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008448 File Offset: 0x00006648
		private ISqlSnippet CreateSqlSnippetForInteger()
		{
			if (this.m_literalNode.Cardinality == Cardinality.One)
			{
				return this.m_sqlBatch.CreateSqlSnippetForInteger(this.m_literalNode.ValueAsInteger);
			}
			SqlLiteralExpression.LiteralSet literalSet = new SqlLiteralExpression.LiteralSet();
			foreach (long num in this.m_literalNode.ValueAsIntegerSet)
			{
				literalSet.Tuples.Add(this.m_sqlBatch.CreateSqlSnippetForInteger(num));
			}
			return literalSet;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000084D8 File Offset: 0x000066D8
		private ISqlSnippet CreateSqlSnippetForDecimal()
		{
			if (this.m_literalNode.Cardinality == Cardinality.One)
			{
				return this.CreateSqlSnippetForDecimal(this.m_literalNode.ValueAsDecimal);
			}
			SqlLiteralExpression.LiteralSet literalSet = new SqlLiteralExpression.LiteralSet();
			foreach (decimal num in this.m_literalNode.ValueAsDecimalSet)
			{
				literalSet.Tuples.Add(this.CreateSqlSnippetForDecimal(num));
			}
			return literalSet;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000855C File Offset: 0x0000675C
		private ISqlSnippet CreateSqlSnippetForDecimal(decimal dec)
		{
			int num = (decimal.GetBits(dec)[3] >> 16) & 255;
			return SqlFunctionExpression.CastAsDecimal(new SqlStringSnippet(dec.ToString(CultureInfo.InvariantCulture)), num);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008594 File Offset: 0x00006794
		private ISqlSnippet CreateSqlSnippetForFloat()
		{
			if (this.m_literalNode.Cardinality == Cardinality.One)
			{
				return this.CreateSqlSnippetForFloat(this.m_literalNode.ValueAsFloat);
			}
			SqlLiteralExpression.LiteralSet literalSet = new SqlLiteralExpression.LiteralSet();
			foreach (double num in this.m_literalNode.ValueAsFloatSet)
			{
				literalSet.Tuples.Add(this.CreateSqlSnippetForFloat(num));
			}
			return literalSet;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008618 File Offset: 0x00006818
		private ISqlSnippet CreateSqlSnippetForFloat(double dbl)
		{
			return SqlFunctionExpression.CastAsFloat(new SqlStringSnippet(XmlConvert.ToString(dbl)));
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000862A File Offset: 0x0000682A
		private ISqlSnippet CreateSqlSnippetForReal(float flt)
		{
			return SqlFunctionExpression.CastAsReal(new SqlStringSnippet(XmlConvert.ToString(flt)));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000863C File Offset: 0x0000683C
		private ISqlSnippet CreateSqlSnippetForBoolean()
		{
			if (this.m_literalNode.Cardinality == Cardinality.One)
			{
				return this.CreateSqlSnippetForBoolean(this.m_literalNode.ValueAsBoolean);
			}
			SqlLiteralExpression.LiteralSet literalSet = new SqlLiteralExpression.LiteralSet();
			foreach (bool flag in this.m_literalNode.ValueAsBooleanSet)
			{
				literalSet.Tuples.Add(this.CreateSqlSnippetForBoolean(flag));
			}
			return literalSet;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000086C0 File Offset: 0x000068C0
		private ISqlSnippet CreateSqlSnippetForDateTime()
		{
			if (this.m_literalNode.Cardinality == Cardinality.One)
			{
				return this.CreateBasicSqlSnippetForDateTime(this.m_literalNode.ValueAsDateTime);
			}
			SqlLiteralExpression.LiteralSet literalSet = new SqlLiteralExpression.LiteralSet();
			foreach (DateTime dateTime in this.m_literalNode.ValueAsDateTimeSet)
			{
				literalSet.Tuples.Add(this.CreateBasicSqlSnippetForDateTime(dateTime));
			}
			return literalSet;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008744 File Offset: 0x00006944
		private ISqlSnippet CreateSqlSnippetForTime()
		{
			if (this.m_literalNode.Cardinality == Cardinality.One)
			{
				return this.CreateBasicSqlSnippetForTime(this.m_literalNode.ValueAsTime);
			}
			SqlLiteralExpression.LiteralSet literalSet = new SqlLiteralExpression.LiteralSet();
			foreach (TimeSpan timeSpan in this.m_literalNode.ValueAsTimeSet)
			{
				literalSet.Tuples.Add(this.CreateBasicSqlSnippetForTime(timeSpan));
			}
			return literalSet;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000087C8 File Offset: 0x000069C8
		private void CreateSqlSnippetsForEntityKey()
		{
			if (this.m_literalNode.Cardinality == Cardinality.One)
			{
				base.Values.AddRange(this.CreateSqlSnippetsForEntityKey(this.m_literalNode.ValueAsEntityKey));
				return;
			}
			SqlLiteralExpression.LiteralSet literalSet = new SqlLiteralExpression.LiteralSet();
			foreach (EntityKey entityKey in this.m_literalNode.ValueAsEntityKeySet)
			{
				SqlTupleExpression sqlTupleExpression = new SqlTupleExpression();
				foreach (ISqlSnippet sqlSnippet in this.CreateSqlSnippetsForEntityKey(entityKey))
				{
					sqlTupleExpression.AddTupleValue(sqlSnippet, false);
				}
				if (sqlTupleExpression.Values.Count == 0)
				{
					throw SQEAssert.AssertFalseAndThrow("Generated entity key tuple has no values.", Array.Empty<object>());
				}
				if (sqlTupleExpression.Values.Count == 1)
				{
					literalSet.Tuples.Add(sqlTupleExpression.Values[0]);
				}
				else
				{
					literalSet.Tuples.Add(sqlTupleExpression);
				}
			}
			base.Values.Add(literalSet);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000088F0 File Offset: 0x00006AF0
		private IEnumerable<ISqlSnippet> CreateSqlSnippetsForEntityKey(EntityKey entityKey)
		{
			if (this.m_entityKeyTarget != null)
			{
				Type[] keyPartTypes = QueryPlanBuilder.GetEntityKeyPartTypes(this.m_entityKeyTarget);
				object[] keyParts = entityKey.ToKeyParts(keyPartTypes, this.m_entityKeyTarget);
				int num;
				for (int i = 0; i < keyParts.Length; i = num)
				{
					yield return this.CreateSqlSnippetForEntityKeyPart(keyParts[i], keyPartTypes[i]);
					num = i + 1;
				}
				keyPartTypes = null;
				keyParts = null;
			}
			else
			{
				yield return this.CreateBasicSqlSnippetForString(entityKey.ToBase64String());
			}
			yield break;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008907 File Offset: 0x00006B07
		private ISqlSnippet CreateSqlSnippetForEntityKeyPart(object keyPart, Type keyPartType)
		{
			if (keyPart == null)
			{
				return SqlNullExpression.SqlNull;
			}
			return DataTypeMapper.PerformAction<ISqlSnippet>(keyPartType, new SqlLiteralExpression.CreateSqlSnippetForEntityKeyPartAction(keyPart, this));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008920 File Offset: 0x00006B20
		private static bool ComputeIsNullable(IQPExpressionInfo qpInfo)
		{
			bool nullable = qpInfo.Nullable;
			IQueryEntity entityKeyTarget = qpInfo.Expression.GetResultType().EntityKeyTarget;
			return nullable | (entityKeyTarget != null && entityKeyTarget.EntityRefIsNullable);
		}

		// Token: 0x040000C7 RID: 199
		private readonly LiteralNode m_literalNode;

		// Token: 0x040000C8 RID: 200
		private readonly IQueryEntity m_entityKeyTarget;

		// Token: 0x040000C9 RID: 201
		private readonly SqlBatch m_sqlBatch;

		// Token: 0x020000B8 RID: 184
		private sealed class CreateSqlSnippetForEntityKeyPartAction : IMappedClrTypeAction<ISqlSnippet>
		{
			// Token: 0x060006B0 RID: 1712 RVA: 0x0001ACE3 File Offset: 0x00018EE3
			internal CreateSqlSnippetForEntityKeyPartAction(object keyPart, SqlLiteralExpression sqlExpression)
			{
				this.m_keyPart = keyPart;
				this.m_sqlExpression = sqlExpression;
			}

			// Token: 0x060006B1 RID: 1713 RVA: 0x0001ACF9 File Offset: 0x00018EF9
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForString()
			{
				return this.m_sqlExpression.CreateBasicSqlSnippetForString((string)this.m_keyPart);
			}

			// Token: 0x060006B2 RID: 1714 RVA: 0x0001AD11 File Offset: 0x00018F11
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForChar()
			{
				return this.m_sqlExpression.CreateBasicSqlSnippetForString(this.m_keyPart.ToString());
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x0001AD2C File Offset: 0x00018F2C
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForInt32()
			{
				return new SqlStringSnippet(((int)this.m_keyPart).ToString(CultureInfo.InvariantCulture));
			}

			// Token: 0x060006B4 RID: 1716 RVA: 0x0001AD58 File Offset: 0x00018F58
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForInt16()
			{
				return new SqlStringSnippet(((short)this.m_keyPart).ToString(CultureInfo.InvariantCulture));
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x0001AD84 File Offset: 0x00018F84
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForUInt16()
			{
				return new SqlStringSnippet(((ushort)this.m_keyPart).ToString(CultureInfo.InvariantCulture));
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x0001ADB0 File Offset: 0x00018FB0
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForByte()
			{
				return new SqlStringSnippet(((byte)this.m_keyPart).ToString(CultureInfo.InvariantCulture));
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x0001ADDC File Offset: 0x00018FDC
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForSByte()
			{
				return new SqlStringSnippet(((sbyte)this.m_keyPart).ToString(CultureInfo.InvariantCulture));
			}

			// Token: 0x060006B8 RID: 1720 RVA: 0x0001AE06 File Offset: 0x00019006
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForDecimal()
			{
				return this.m_sqlExpression.CreateSqlSnippetForDecimal((decimal)this.m_keyPart);
			}

			// Token: 0x060006B9 RID: 1721 RVA: 0x0001AE20 File Offset: 0x00019020
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForInt64()
			{
				return new SqlStringSnippet(((long)this.m_keyPart).ToString(CultureInfo.InvariantCulture));
			}

			// Token: 0x060006BA RID: 1722 RVA: 0x0001AE4C File Offset: 0x0001904C
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForUInt64()
			{
				return new SqlStringSnippet(((ulong)this.m_keyPart).ToString(CultureInfo.InvariantCulture));
			}

			// Token: 0x060006BB RID: 1723 RVA: 0x0001AE78 File Offset: 0x00019078
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForUInt32()
			{
				return new SqlStringSnippet(((uint)this.m_keyPart).ToString(CultureInfo.InvariantCulture));
			}

			// Token: 0x060006BC RID: 1724 RVA: 0x0001AEA2 File Offset: 0x000190A2
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForDouble()
			{
				return this.m_sqlExpression.CreateSqlSnippetForFloat((double)this.m_keyPart);
			}

			// Token: 0x060006BD RID: 1725 RVA: 0x0001AEBA File Offset: 0x000190BA
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForSingle()
			{
				return this.m_sqlExpression.CreateSqlSnippetForReal((float)this.m_keyPart);
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x0001AED2 File Offset: 0x000190D2
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForBoolean()
			{
				return this.m_sqlExpression.CreateSqlSnippetForBoolean((bool)this.m_keyPart);
			}

			// Token: 0x060006BF RID: 1727 RVA: 0x0001AEEA File Offset: 0x000190EA
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForDateTime()
			{
				return this.m_sqlExpression.CreateBasicSqlSnippetForDateTime((DateTime)this.m_keyPart);
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x0001AEEA File Offset: 0x000190EA
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForDateTimeOffset()
			{
				return this.m_sqlExpression.CreateBasicSqlSnippetForDateTime((DateTime)this.m_keyPart);
			}

			// Token: 0x060006C1 RID: 1729 RVA: 0x0001AF02 File Offset: 0x00019102
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForTimeSpan()
			{
				return this.m_sqlExpression.CreateBasicSqlSnippetForTime((TimeSpan)this.m_keyPart);
			}

			// Token: 0x060006C2 RID: 1730 RVA: 0x0001AF1A File Offset: 0x0001911A
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForGuid()
			{
				return new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CastOpenParenSnippet,
					this.m_sqlExpression.CreateBasicSqlSnippetForString(this.m_keyPart.ToString()),
					SqlExpression.AsGuidCloseParenSnippet
				});
			}

			// Token: 0x060006C3 RID: 1731 RVA: 0x0001AF50 File Offset: 0x00019150
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForByteArray()
			{
				byte[] array = (byte[])this.m_keyPart;
				StringBuilder stringBuilder = new StringBuilder(array.Length * 2 + 2);
				stringBuilder.Append("0x");
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] < 16)
					{
						stringBuilder.Append("0");
					}
					stringBuilder.Append(array[i].ToString("X"));
				}
				return new SqlStringSnippet(stringBuilder.ToString());
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x0001AFC6 File Offset: 0x000191C6
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForEntityKey()
			{
				throw SQEAssert.AssertFalseAndThrow("EntityKey object can not be a part of another entity key.", Array.Empty<object>());
			}

			// Token: 0x060006C5 RID: 1733 RVA: 0x0001AFD7 File Offset: 0x000191D7
			ISqlSnippet IMappedClrTypeAction<ISqlSnippet>.ForUnknown()
			{
				throw SQEAssert.AssertFalseAndThrow("Uknown type of entity key part.", Array.Empty<object>());
			}

			// Token: 0x0400034C RID: 844
			private readonly object m_keyPart;

			// Token: 0x0400034D RID: 845
			private readonly SqlLiteralExpression m_sqlExpression;
		}

		// Token: 0x020000B9 RID: 185
		internal sealed class LiteralSet : ISqlSnippet
		{
			// Token: 0x060006C6 RID: 1734 RVA: 0x0001AFE8 File Offset: 0x000191E8
			internal LiteralSet()
			{
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001AFFB File Offset: 0x000191FB
			internal SqlSnippetCollection Tuples
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_tuples;
				}
			}

			// Token: 0x060006C8 RID: 1736 RVA: 0x0001B003 File Offset: 0x00019203
			void ISqlSnippet.Compile(FormattedStringWriter fsw)
			{
				throw SQEAssert.AssertFalseAndThrow("LiteralSet can not be compiled.", Array.Empty<object>());
			}

			// Token: 0x0400034E RID: 846
			private readonly SqlSnippetCollection m_tuples = new SqlSnippetCollection();
		}
	}
}
