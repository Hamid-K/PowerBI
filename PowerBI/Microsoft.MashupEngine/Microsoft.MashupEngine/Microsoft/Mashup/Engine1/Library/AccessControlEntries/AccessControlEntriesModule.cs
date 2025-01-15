using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AccessControlEntries
{
	// Token: 0x02001238 RID: 4664
	internal sealed class AccessControlEntriesModule : Module
	{
		// Token: 0x170021AC RID: 8620
		// (get) Token: 0x06007B32 RID: 31538 RVA: 0x001A9364 File Offset: 0x001A7564
		public override string Name
		{
			get
			{
				return "AccessControlEntries";
			}
		}

		// Token: 0x170021AD RID: 8621
		// (get) Token: 0x06007B33 RID: 31539 RVA: 0x001A936B File Offset: 0x001A756B
		public override Keys ExportKeys
		{
			get
			{
				if (AccessControlEntriesModule.exportKeys == null)
				{
					AccessControlEntriesModule.exportKeys = Keys.New(11, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "AccessControlEntry.Type";
						case 1:
							return "AccessControlEntry.ConditionContextType";
						case 2:
							return "AccessControlEntry.ConditionToIdentities";
						case 3:
							return AccessControlEntriesModule.AccessControlKind.Type.GetName();
						case 4:
							return AccessControlEntriesModule.AccessControlKind.Allow.GetName();
						case 5:
							return AccessControlEntriesModule.AccessControlKind.Deny.GetName();
						case 6:
							return "IdentityProvider.Type";
						case 7:
							return "IdentityProvider.Default";
						case 8:
							return "Identity.Type";
						case 9:
							return "Identity.From";
						case 10:
							return "Identity.IsMemberOf";
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					});
				}
				return AccessControlEntriesModule.exportKeys;
			}
		}

		// Token: 0x06007B34 RID: 31540 RVA: 0x001A93A4 File Offset: 0x001A75A4
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return AccessControlEntriesModule.AccessControlEntry.Type;
				case 1:
					return AccessControlEntriesModule.AccessControlEntry.ConditionContextType;
				case 2:
					return AccessControlEntriesModule.AccessControlEntry.ConditionToIdentities;
				case 3:
					return AccessControlEntriesModule.AccessControlKind.Type;
				case 4:
					return AccessControlEntriesModule.AccessControlKind.Allow;
				case 5:
					return AccessControlEntriesModule.AccessControlKind.Deny;
				case 6:
					return AccessControlEntriesModule.IdentityProvider.Type;
				case 7:
					return AccessControlEntriesModule.IdentityProvider.Default;
				case 8:
					return AccessControlEntriesModule.Identity.Type;
				case 9:
					return AccessControlEntriesModule.Identity.From;
				case 10:
					return AccessControlEntriesModule.Identity.IsMemberOf;
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x04004441 RID: 17473
		private static Keys exportKeys;

		// Token: 0x02001239 RID: 4665
		private enum Exports
		{
			// Token: 0x04004443 RID: 17475
			AccessControlEntry_Type,
			// Token: 0x04004444 RID: 17476
			AccessControlEntry_ConditionContextType,
			// Token: 0x04004445 RID: 17477
			AccessControlEntry_ConditionToIdentities,
			// Token: 0x04004446 RID: 17478
			AccessControlKind_Type,
			// Token: 0x04004447 RID: 17479
			AccessControlKind_Allow,
			// Token: 0x04004448 RID: 17480
			AccessControlKind_Deny,
			// Token: 0x04004449 RID: 17481
			IdentityProvider_Type,
			// Token: 0x0400444A RID: 17482
			IdentityProvider_Default,
			// Token: 0x0400444B RID: 17483
			Identity_Type,
			// Token: 0x0400444C RID: 17484
			Identity_From,
			// Token: 0x0400444D RID: 17485
			Identity_IsMemberOf,
			// Token: 0x0400444E RID: 17486
			Count
		}

		// Token: 0x0200123A RID: 4666
		public static class AccessControlEntry
		{
			// Token: 0x06007B36 RID: 31542 RVA: 0x001A93D0 File Offset: 0x001A75D0
			private static IExpression NewContextFieldRef(Identifier fieldName)
			{
				return new RequiredFieldAccessExpressionSyntaxNode(AccessControlEntriesModule.AccessControlEntry.contextRef, fieldName);
			}

			// Token: 0x0400444F RID: 17487
			public const string TableKey = "AccessControlEntry.Table";

			// Token: 0x04004450 RID: 17488
			public static readonly Keys TableMetadataKeys = Keys.New("AccessControlEntry.Table");

			// Token: 0x04004451 RID: 17489
			public const string IdentityProviderKey = "IdentityProvider";

			// Token: 0x04004452 RID: 17490
			public const string UserKey = "User";

			// Token: 0x04004453 RID: 17491
			public const string ActionArgumentsKey = "ActionArguments";

			// Token: 0x04004454 RID: 17492
			public static readonly RecordTypeValue ConditionContextType = RecordTypeValue.New(RecordValue.New(Keys.New("IdentityProvider", "User", "ActionArguments"), new Value[]
			{
				RecordTypeValue.NewField(AccessControlEntriesModule.IdentityProvider.Type, null),
				RecordTypeValue.NewField(AccessControlEntriesModule.Identity.Type, null),
				RecordTypeValue.NewField(TypeValue.List, null)
			}));

			// Token: 0x04004455 RID: 17493
			public static readonly FunctionTypeValue ConditionType = FunctionTypeValue.New(TypeValue.Logical, RecordValue.New(Keys.New("context"), new Value[] { AccessControlEntriesModule.AccessControlEntry.ConditionContextType }), 1);

			// Token: 0x04004456 RID: 17494
			public static readonly RecordTypeValue Type = RecordTypeValue.New(RecordValue.New(Keys.New("AccessControlKind", "Action", "Condition"), new Value[]
			{
				RecordTypeValue.NewField(AccessControlEntriesModule.AccessControlKind.Type, null),
				RecordTypeValue.NewField(TypeValue.Function, null),
				RecordTypeValue.NewField(AccessControlEntriesModule.AccessControlEntry.ConditionType, null)
			}));

			// Token: 0x04004457 RID: 17495
			public static readonly TableTypeValue TableType = TableTypeValue.New(AccessControlEntriesModule.AccessControlEntry.Type);

			// Token: 0x04004458 RID: 17496
			public static readonly FunctionValue ConditionToIdentities = new AccessControlEntriesModule.AccessControlEntry.ConditionToIdentitiesFunctionValue();

			// Token: 0x04004459 RID: 17497
			private static readonly IFunctionTypeExpression conditionFunctionTypeExpr = new FunctionTypeSyntaxNode(new ConstantExpressionSyntaxNode(TypeValue.Logical), new IParameter[]
			{
				new ParameterSyntaxNode(Identifier.New("context"), new ConstantExpressionSyntaxNode(AccessControlEntriesModule.AccessControlEntry.ConditionContextType))
			}, 1);

			// Token: 0x0400445A RID: 17498
			private static readonly IExpression contextRef = new InclusiveIdentifierExpressionSyntaxNode(AccessControlEntriesModule.AccessControlEntry.conditionFunctionTypeExpr.Parameters[0].Identifier);

			// Token: 0x0400445B RID: 17499
			private static readonly Identifier identityProviderFieldName = Identifier.New("IdentityProvider");

			// Token: 0x0400445C RID: 17500
			private static readonly Identifier userFieldName = Identifier.New("User");

			// Token: 0x0200123B RID: 4667
			private interface ICacheIdentities
			{
				// Token: 0x06007B38 RID: 31544
				ListValue GetIdentities(FunctionValue identityProvider);
			}

			// Token: 0x0200123C RID: 4668
			private class ConditionToIdentitiesFunctionValue : NativeFunctionValue2<ListValue, FunctionValue, FunctionValue>
			{
				// Token: 0x06007B39 RID: 31545 RVA: 0x001A955C File Offset: 0x001A775C
				public ConditionToIdentitiesFunctionValue()
					: base(ListTypeValue.New(AccessControlEntriesModule.Identity.Type), "identityProvider", AccessControlEntriesModule.IdentityProvider.Type, "condition", AccessControlEntriesModule.AccessControlEntry.ConditionType)
				{
				}

				// Token: 0x06007B3A RID: 31546 RVA: 0x001A9584 File Offset: 0x001A7784
				public override ListValue TypedInvoke(FunctionValue identityProvider, FunctionValue condition)
				{
					AccessControlEntriesModule.AccessControlEntry.ICacheIdentities cacheIdentities = condition as AccessControlEntriesModule.AccessControlEntry.ICacheIdentities;
					if (cacheIdentities != null)
					{
						return cacheIdentities.GetIdentities(identityProvider);
					}
					Value value;
					if (AccessControlEntriesModule.AccessControlEntry.ConditionToIdentitiesFunctionValue.TryGetIdentityToken(condition, out value))
					{
						return ListValue.New(new Value[] { AccessControlEntriesModule.Identity.From.Invoke(identityProvider, value) });
					}
					throw ValueException.NewExpressionError<Message0>(Strings.AccessControlEntry_ConditionConversionToIdentitiesNotSupported, null, null);
				}

				// Token: 0x06007B3B RID: 31547 RVA: 0x001A95D4 File Offset: 0x001A77D4
				private static bool TryGetIdentityToken(FunctionValue condition, out Value identityToken)
				{
					IFunctionExpression functionExpression = condition.Expression as IFunctionExpression;
					if (functionExpression != null)
					{
						functionExpression = (IFunctionExpression)NormalizationVisitor.Normalize(functionExpression, new TypeValue[] { AccessControlEntriesModule.AccessControlEntry.ConditionContextType }, true);
						Dictionary<string, IExpression> dictionary;
						if (AccessControlEntriesModule.AccessControlEntry.ConditionToIdentitiesFunctionValue.identityPattern.TryMatch(functionExpression, out dictionary) && dictionary["token"].TryGetConstant(out identityToken))
						{
							return true;
						}
					}
					identityToken = null;
					return false;
				}

				// Token: 0x0400445D RID: 17501
				private static readonly ExpressionPattern identityPattern = new ExpressionPattern(new string[] { "(context) => context[User] = Identity.From(context[IdentityProvider], __token)", "(context) => Identity.From(context[IdentityProvider], __token) = context[User]", "(context) => Identity.IsMemberOf(context[User], Identity.From(context[IdentityProvider], __token))" }).Bind(RecordValue.New(Keys.New("Identity.From", "Identity.IsMemberOf"), new Value[]
				{
					AccessControlEntriesModule.Identity.From,
					AccessControlEntriesModule.Identity.IsMemberOf
				}));
			}

			// Token: 0x0200123D RID: 4669
			public sealed class UserEqualsConditionFunctionValue : NativeFunctionValue1<LogicalValue, RecordValue>, AccessControlEntriesModule.AccessControlEntry.ICacheIdentities
			{
				// Token: 0x06007B3D RID: 31549 RVA: 0x001A9698 File Offset: 0x001A7898
				public UserEqualsConditionFunctionValue(Value identityToken)
					: base(TypeValue.Logical, "context", AccessControlEntriesModule.AccessControlEntry.ConditionContextType)
				{
					this.identityToken = identityToken;
				}

				// Token: 0x06007B3E RID: 31550 RVA: 0x001A96B8 File Offset: 0x001A78B8
				public ListValue GetIdentities(FunctionValue identityProvider)
				{
					if (this.identities == null || !this.identities.Item0["IdentityProvider"].Equals(identityProvider))
					{
						this.identities = ListValue.New(new Value[] { AccessControlEntriesModule.Identity.From.Invoke(identityProvider, this.identityToken) });
					}
					return this.identities;
				}

				// Token: 0x170021AE RID: 8622
				// (get) Token: 0x06007B3F RID: 31551 RVA: 0x001A9718 File Offset: 0x001A7918
				public override IExpression Expression
				{
					get
					{
						if (this.expression == null)
						{
							this.expression = new FunctionExpressionSyntaxNode(AccessControlEntriesModule.AccessControlEntry.conditionFunctionTypeExpr, BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, AccessControlEntriesModule.AccessControlEntry.NewContextFieldRef(AccessControlEntriesModule.AccessControlEntry.userFieldName), new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(AccessControlEntriesModule.Identity.From), AccessControlEntriesModule.AccessControlEntry.NewContextFieldRef(AccessControlEntriesModule.AccessControlEntry.identityProviderFieldName), new ConstantExpressionSyntaxNode(this.identityToken)), TokenRange.Null));
						}
						return this.expression;
					}
				}

				// Token: 0x06007B40 RID: 31552 RVA: 0x001A977C File Offset: 0x001A797C
				public override LogicalValue TypedInvoke(RecordValue context)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Identity_NotSupportedAtRuntime, null, null);
				}

				// Token: 0x0400445E RID: 17502
				private readonly Value identityToken;

				// Token: 0x0400445F RID: 17503
				private ListValue identities;

				// Token: 0x04004460 RID: 17504
				private IExpression expression;
			}

			// Token: 0x0200123E RID: 4670
			public sealed class IsMemberOfConditionFunctionValue : NativeFunctionValue1<LogicalValue, RecordValue>, AccessControlEntriesModule.AccessControlEntry.ICacheIdentities
			{
				// Token: 0x06007B41 RID: 31553 RVA: 0x001A978A File Offset: 0x001A798A
				public IsMemberOfConditionFunctionValue(Value identityToken)
					: base(TypeValue.Logical, "context", AccessControlEntriesModule.AccessControlEntry.ConditionContextType)
				{
					this.identityToken = identityToken;
				}

				// Token: 0x06007B42 RID: 31554 RVA: 0x001A97A8 File Offset: 0x001A79A8
				public ListValue GetIdentities(FunctionValue identityProvider)
				{
					if (this.identities == null || !this.identities.Item0["IdentityProvider"].Equals(identityProvider))
					{
						this.identities = ListValue.New(new Value[] { AccessControlEntriesModule.Identity.From.Invoke(identityProvider, this.identityToken) });
					}
					return this.identities;
				}

				// Token: 0x170021AF RID: 8623
				// (get) Token: 0x06007B43 RID: 31555 RVA: 0x001A9808 File Offset: 0x001A7A08
				public override IExpression Expression
				{
					get
					{
						if (this.expression == null)
						{
							this.expression = new FunctionExpressionSyntaxNode(AccessControlEntriesModule.AccessControlEntry.conditionFunctionTypeExpr, new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(AccessControlEntriesModule.Identity.IsMemberOf), AccessControlEntriesModule.AccessControlEntry.NewContextFieldRef(AccessControlEntriesModule.AccessControlEntry.userFieldName), new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(AccessControlEntriesModule.Identity.From), AccessControlEntriesModule.AccessControlEntry.NewContextFieldRef(AccessControlEntriesModule.AccessControlEntry.identityProviderFieldName), new ConstantExpressionSyntaxNode(this.identityToken))));
						}
						return this.expression;
					}
				}

				// Token: 0x06007B44 RID: 31556 RVA: 0x001A977C File Offset: 0x001A797C
				public override LogicalValue TypedInvoke(RecordValue context)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Identity_NotSupportedAtRuntime, null, null);
				}

				// Token: 0x04004461 RID: 17505
				private readonly Value identityToken;

				// Token: 0x04004462 RID: 17506
				private ListValue identities;

				// Token: 0x04004463 RID: 17507
				private IExpression expression;
			}
		}

		// Token: 0x0200123F RID: 4671
		public static class AccessControlKind
		{
			// Token: 0x04004464 RID: 17508
			public static readonly IntEnumTypeValue<AccessControlEntriesModule.AccessControlKind.Kind> Type = new IntEnumTypeValue<AccessControlEntriesModule.AccessControlKind.Kind>("AccessControlKind.Type");

			// Token: 0x04004465 RID: 17509
			public static readonly NumberValue Deny = AccessControlEntriesModule.AccessControlKind.Type.NewEnumValue("AccessControlKind.Deny", 0, AccessControlEntriesModule.AccessControlKind.Kind.Deny, null);

			// Token: 0x04004466 RID: 17510
			public static readonly NumberValue Allow = AccessControlEntriesModule.AccessControlKind.Type.NewEnumValue("AccessControlKind.Allow", 1, AccessControlEntriesModule.AccessControlKind.Kind.Allow, null);

			// Token: 0x02001240 RID: 4672
			public enum Kind
			{
				// Token: 0x04004468 RID: 17512
				Deny,
				// Token: 0x04004469 RID: 17513
				Allow
			}
		}

		// Token: 0x02001241 RID: 4673
		public static class IdentityProvider
		{
			// Token: 0x0400446A RID: 17514
			public static readonly TypeValue Type = FunctionTypeValue.New(TypeValue.Any, RecordValue.Empty, 0);

			// Token: 0x0400446B RID: 17515
			public static readonly FunctionValue Default = new AccessControlEntriesModule.IdentityProvider.DefaultFunctionValue();

			// Token: 0x02001242 RID: 4674
			private class DefaultFunctionValue : NativeFunctionValue0<Value>
			{
				// Token: 0x06007B47 RID: 31559 RVA: 0x001A98D0 File Offset: 0x001A7AD0
				public DefaultFunctionValue()
					: base(TypeValue.Any)
				{
				}

				// Token: 0x06007B48 RID: 31560 RVA: 0x001A977C File Offset: 0x001A797C
				public override Value TypedInvoke()
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Identity_NotSupportedAtRuntime, null, null);
				}
			}
		}

		// Token: 0x02001243 RID: 4675
		public static class Identity
		{
			// Token: 0x0400446C RID: 17516
			public static readonly RecordTypeValue Type = RecordTypeValue.New(RecordValue.New(Keys.New("IdentityProvider", "Token"), new Value[]
			{
				RecordTypeValue.NewField(AccessControlEntriesModule.IdentityProvider.Type, null),
				RecordTypeValue.NewField(TypeValue.Any, null)
			}));

			// Token: 0x0400446D RID: 17517
			public static readonly FunctionValue From = new AccessControlEntriesModule.Identity.FromFunctionValue();

			// Token: 0x0400446E RID: 17518
			public static readonly FunctionValue IsMemberOf = new AccessControlEntriesModule.Identity.IsMemberOfFunctionValue();

			// Token: 0x02001244 RID: 4676
			private class FromFunctionValue : NativeFunctionValue2<RecordValue, FunctionValue, Value>
			{
				// Token: 0x06007B4A RID: 31562 RVA: 0x001A9941 File Offset: 0x001A7B41
				public FromFunctionValue()
					: base(AccessControlEntriesModule.Identity.Type, "identityProvider", AccessControlEntriesModule.IdentityProvider.Type, "value", TypeValue.Any)
				{
				}

				// Token: 0x06007B4B RID: 31563 RVA: 0x001A9962 File Offset: 0x001A7B62
				public override RecordValue TypedInvoke(FunctionValue identityProvider, Value value)
				{
					if (identityProvider.FunctionIdentity.Equals(AccessControlEntriesModule.IdentityProvider.Default.FunctionIdentity))
					{
						return RecordValue.New(AccessControlEntriesModule.Identity.Type, new Value[] { identityProvider, value });
					}
					throw ValueException.NewExpressionError<Message0>(Strings.IdentityProvider_NotSupported, identityProvider, null);
				}
			}

			// Token: 0x02001245 RID: 4677
			private class IsMemberOfFunctionValue : NativeFunctionValue2<LogicalValue, RecordValue, RecordValue>
			{
				// Token: 0x06007B4C RID: 31564 RVA: 0x001A99A0 File Offset: 0x001A7BA0
				public IsMemberOfFunctionValue()
					: base(TypeValue.Logical, "identity", AccessControlEntriesModule.Identity.Type, "collection", AccessControlEntriesModule.Identity.Type)
				{
				}

				// Token: 0x06007B4D RID: 31565 RVA: 0x001A977C File Offset: 0x001A797C
				public override LogicalValue TypedInvoke(RecordValue identity, RecordValue collection)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Identity_NotSupportedAtRuntime, null, null);
				}
			}
		}
	}
}
