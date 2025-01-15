using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x02000515 RID: 1301
	internal sealed class ResourceModule : Module
	{
		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x0007E3B4 File Offset: 0x0007C5B4
		public override string Name
		{
			get
			{
				return "Resource";
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06002A24 RID: 10788 RVA: 0x0007E3BB File Offset: 0x0007C5BB
		public override Keys ExportKeys
		{
			get
			{
				if (ResourceModule.exportKeys == null)
				{
					ResourceModule.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Value.ResourceExpression";
						}
						if (index != 1)
						{
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
						return "Resource.Access";
					});
				}
				return ResourceModule.exportKeys;
			}
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x0007E3F3 File Offset: 0x0007C5F3
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return ResourceModule._Value.ResourceExpression;
				}
				if (index != 1)
				{
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				return ResourceModule.Resource.Access;
			});
		}

		// Token: 0x0400124F RID: 4687
		private static Keys exportKeys;

		// Token: 0x02000516 RID: 1302
		private enum Exports
		{
			// Token: 0x04001251 RID: 4689
			Value_ResourceExpression,
			// Token: 0x04001252 RID: 4690
			Access,
			// Token: 0x04001253 RID: 4691
			Count
		}

		// Token: 0x02000517 RID: 1303
		public static class _Value
		{
			// Token: 0x04001254 RID: 4692
			public static readonly FunctionValue ResourceExpression = new ResourceModule._Value.ResourceExpressionFunctionValue();

			// Token: 0x02000518 RID: 1304
			private class ResourceExpressionFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06002A28 RID: 10792 RVA: 0x0007E42B File Offset: 0x0007C62B
				public ResourceExpressionFunctionValue()
					: base(TypeValue.Any, "value", TypeValue.Any)
				{
				}

				// Token: 0x06002A29 RID: 10793 RVA: 0x0007E444 File Offset: 0x0007C644
				public override Value TypedInvoke(Value value)
				{
					IExpression expression = value.Expression;
					if (expression == null)
					{
						expression = new ConstantExpressionSyntaxNode(value);
					}
					return ExpressionToMAstVisitor.ToMAst(expression);
				}
			}
		}

		// Token: 0x02000519 RID: 1305
		public static class Resource
		{
			// Token: 0x04001255 RID: 4693
			public static readonly FunctionValue Access = new ResourceModule.Resource.AccessFunctionValue();

			// Token: 0x0200051A RID: 1306
			private sealed class AccessFunctionValue : NativeFunctionValue3<Value, Value, Value, Value>
			{
				// Token: 0x06002A2B RID: 10795 RVA: 0x0007E474 File Offset: 0x0007C674
				public AccessFunctionValue()
					: base(TypeValue.Any, 1, "resource", TypeValue.Any, "nativeQuery", TypeValue.Text.Nullable, "options", TypeValue.Record.Nullable)
				{
				}

				// Token: 0x17001014 RID: 4116
				// (get) Token: 0x06002A2C RID: 10796 RVA: 0x0007E4B5 File Offset: 0x0007C6B5
				public override string PrimaryResourceKind
				{
					get
					{
						return string.Empty;
					}
				}

				// Token: 0x06002A2D RID: 10797 RVA: 0x0007E4BC File Offset: 0x0007C6BC
				public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
				{
					Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
					if (argumentValues != null && argumentValues.Length != 0 && argumentValues[0].IsRecord)
					{
						RecordValue asRecord = argumentValues[0].AsRecord;
						ResourceKindInfo resourceKindInfo;
						IResource resource;
						string text;
						if (ResourceKinds.Lookup(asRecord["Kind"].AsString, out resourceKindInfo) && resourceKindInfo.Validate(asRecord["Path"].AsString, out resource, out text))
						{
							location = new InternalDataSourceDataSourceLocation(resource.Kind, resource.Path);
							foundOptions = RecordValue.Empty;
							unknownOptions = Keys.Empty;
							if (argumentValues.Length > 1 && argumentValues[1].IsText)
							{
								location.Query = argumentValues[1].AsString;
							}
							if (argumentValues.Length > 2 && argumentValues[2].IsRecord)
							{
								foundOptions = argumentValues[2].AsRecord;
							}
							return true;
						}
					}
					location = null;
					foundOptions = null;
					unknownOptions = null;
					return false;
				}

				// Token: 0x06002A2E RID: 10798 RVA: 0x0007E590 File Offset: 0x0007C790
				public override Value TypedInvoke(Value resource, Value nativeQuery, Value options)
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.FunctionCannotBeInvoked, this, null);
				}
			}
		}
	}
}
