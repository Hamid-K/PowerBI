using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.SqlTranslator;

namespace Microsoft.Mashup.Engine1.Library.SqlExpression
{
	// Token: 0x020003DA RID: 986
	public sealed class SqlExpressionModule : Module
	{
		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x0005F0AD File Offset: 0x0005D2AD
		public override string Name
		{
			get
			{
				return "SqlExpressionModule";
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x0005F0B4 File Offset: 0x0005D2B4
		public override Keys ExportKeys
		{
			get
			{
				if (SqlExpressionModule.exportKeys == null)
				{
					SqlExpressionModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "SqlExpression.ToExpression";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return SqlExpressionModule.exportKeys;
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x0005F0EC File Offset: 0x0005D2EC
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new SqlExpressionModule.ToExpressionFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x04000D4B RID: 3403
		private static Keys exportKeys;

		// Token: 0x020003DB RID: 987
		private enum Exports
		{
			// Token: 0x04000D4D RID: 3405
			SqlExpression_ToExpression,
			// Token: 0x04000D4E RID: 3406
			Count
		}

		// Token: 0x020003DC RID: 988
		private sealed class ToExpressionFunctionValue : NativeFunctionValue2<TextValue, TextValue, RecordValue>
		{
			// Token: 0x0600224C RID: 8780 RVA: 0x0005F11D File Offset: 0x0005D31D
			public ToExpressionFunctionValue(IEngineHost host)
				: base(TypeValue.Text, "sql", TypeValue.Text, "environment", TypeValue.Record)
			{
				this.host = host;
			}

			// Token: 0x0600224D RID: 8781 RVA: 0x0005F148 File Offset: 0x0005D348
			public override TextValue TypedInvoke(TextValue sql, RecordValue environment)
			{
				SqlExpressionTranslationResult sqlExpressionTranslationResult = SqlExpressionTranslator.Translate(Engine.Instance, this.host, environment, SqlParser.Parse(this.host, sql.AsString));
				if (sqlExpressionTranslationResult.IsSupported)
				{
					return TextValue.New(sqlExpressionTranslationResult.Expression);
				}
				string text = sql.AsString;
				if (text.Length > 1024)
				{
					text = text.Substring(0, 1024);
					text += "...";
				}
				throw ValueException.NewExpressionError<Message1>(Strings.SqlExpression_ExpressionNotSupported(text), sql, null);
			}

			// Token: 0x04000D4F RID: 3407
			private const int sqlTruncateLimit = 1024;

			// Token: 0x04000D50 RID: 3408
			private readonly IEngineHost host;
		}
	}
}
