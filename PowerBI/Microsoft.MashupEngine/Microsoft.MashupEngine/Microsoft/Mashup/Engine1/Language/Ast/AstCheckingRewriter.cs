using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x0200189F RID: 6303
	public sealed class AstCheckingRewriter : AstVisitor
	{
		// Token: 0x06009FFB RID: 40955 RVA: 0x00210A6D File Offset: 0x0020EC6D
		public static IDocument Rewrite(IDocument document, Action<IError> log)
		{
			return new AstCheckingRewriter(document.Host, document.Tokens, log).VisitDocument(document);
		}

		// Token: 0x06009FFC RID: 40956 RVA: 0x00210A87 File Offset: 0x0020EC87
		private AstCheckingRewriter(IDocumentHost host, ITokens tokens, Action<IError> log)
		{
			this.host = host;
			this.tokens = tokens;
			this.log = log;
		}

		// Token: 0x06009FFD RID: 40957 RVA: 0x00210AA4 File Offset: 0x0020ECA4
		private bool TryGetDuplicate(IList<VariableInitializer> initializers, out Identifier identifier)
		{
			HashSet<Identifier> hashSet = new HashSet<Identifier>();
			for (int i = 0; i < initializers.Count; i++)
			{
				Identifier name = initializers[i].Name;
				if (hashSet.Contains(name))
				{
					identifier = name;
					return true;
				}
				hashSet.Add(name);
			}
			identifier = null;
			return false;
		}

		// Token: 0x06009FFE RID: 40958 RVA: 0x00210AF4 File Offset: 0x0020ECF4
		private bool TryGetDuplicate(IList<IFieldType> fieldTypes, out Identifier identifier)
		{
			HashSet<Identifier> hashSet = new HashSet<Identifier>();
			for (int i = 0; i < fieldTypes.Count; i++)
			{
				Identifier name = fieldTypes[i].Name;
				if (hashSet.Contains(name))
				{
					identifier = name;
					return true;
				}
				hashSet.Add(name);
			}
			identifier = null;
			return false;
		}

		// Token: 0x06009FFF RID: 40959 RVA: 0x00210B40 File Offset: 0x0020ED40
		private bool TryGetDuplicate(IList<IParameter> parameters, out Identifier identifier)
		{
			HashSet<Identifier> hashSet = new HashSet<Identifier>();
			for (int i = 0; i < parameters.Count; i++)
			{
				Identifier identifier2 = parameters[i].Identifier;
				if (hashSet.Contains(identifier2))
				{
					identifier = identifier2;
					return true;
				}
				hashSet.Add(identifier2);
			}
			identifier = null;
			return false;
		}

		// Token: 0x0600A000 RID: 40960 RVA: 0x00210B8C File Offset: 0x0020ED8C
		private bool TryGetDuplicate(IList<ISectionMember> members, out Identifier identifier)
		{
			HashSet<Identifier> hashSet = new HashSet<Identifier>();
			for (int i = 0; i < members.Count; i++)
			{
				Identifier name = members[i].Name;
				if (hashSet.Contains(name))
				{
					identifier = name;
					return true;
				}
				hashSet.Add(name);
			}
			identifier = null;
			return false;
		}

		// Token: 0x0600A001 RID: 40961 RVA: 0x00210BD8 File Offset: 0x0020EDD8
		private static int CollectDuplicates(IList<VariableInitializer> variables, HashSet<Identifier> set, HashSet<Identifier> dups)
		{
			int num = 0;
			for (int i = 0; i < variables.Count; i++)
			{
				Identifier name = variables[i].Name;
				if (set.Contains(name))
				{
					dups.Add(name);
					num++;
				}
				set.Add(name);
			}
			return num;
		}

		// Token: 0x0600A002 RID: 40962 RVA: 0x00210C28 File Offset: 0x0020EE28
		private static int CollectDuplicates(IList<ISectionMember> members, HashSet<Identifier> set, HashSet<Identifier> dups)
		{
			int num = 0;
			for (int i = 0; i < members.Count; i++)
			{
				Identifier name = members[i].Name;
				if (set.Contains(name))
				{
					dups.Add(name);
					num++;
				}
				set.Add(name);
			}
			return num;
		}

		// Token: 0x0600A003 RID: 40963 RVA: 0x00210C73 File Offset: 0x0020EE73
		private IExpression InvalidExpression(string message)
		{
			return new ThrowExpressionSyntaxNode(new ConstantExpressionSyntaxNode(ErrorRecord.New(ValueException.ExpressionError, TextValue.New(message), Value.Null)));
		}

		// Token: 0x0600A004 RID: 40964 RVA: 0x00210C94 File Offset: 0x0020EE94
		private IExpression InvalidExpression<T>(T message) where T : IUserMessage
		{
			return new ThrowExpressionSyntaxNode(new ConstantExpressionSyntaxNode(ErrorRecord.New(ValueException.ExpressionError, message, Value.Null)));
		}

		// Token: 0x0600A005 RID: 40965 RVA: 0x00210CB8 File Offset: 0x0020EEB8
		private VariableInitializer[] RemoveDuplicateInitializers(IList<VariableInitializer> members, Func<object, Message1> message)
		{
			HashSet<Identifier> hashSet = new HashSet<Identifier>();
			HashSet<Identifier> hashSet2 = new HashSet<Identifier>();
			int num = AstCheckingRewriter.CollectDuplicates(members, hashSet, hashSet2);
			VariableInitializer[] array = new VariableInitializer[members.Count - num];
			int num2 = 0;
			for (int i = 0; i < members.Count; i++)
			{
				Identifier name = members[i].Name;
				if (hashSet.Contains(name))
				{
					if (hashSet2.Contains(name))
					{
						IExpression expression = this.InvalidExpression<Message1>(message(name.Name));
						array[num2++] = new VariableInitializer(name, expression);
					}
					else
					{
						array[num2++] = base.VisitInitializer(members[i]);
					}
					hashSet.Remove(name);
				}
			}
			return array;
		}

		// Token: 0x0600A006 RID: 40966 RVA: 0x00210D7C File Offset: 0x0020EF7C
		private IList<ISectionMember> RemoveDuplicateModuleMembers(IList<ISectionMember> members, Func<object, Message1> messageFormatter)
		{
			HashSet<Identifier> hashSet = new HashSet<Identifier>();
			HashSet<Identifier> hashSet2 = new HashSet<Identifier>();
			int num = AstCheckingRewriter.CollectDuplicates(members, hashSet, hashSet2);
			ISectionMember[] array = new ISectionMember[members.Count - num];
			int num2 = 0;
			for (int i = 0; i < members.Count; i++)
			{
				Identifier name = members[i].Name;
				if (hashSet.Contains(name))
				{
					if (hashSet2.Contains(name))
					{
						IExpression expression = this.InvalidExpression<Message1>(messageFormatter(name.Name));
						array[num2++] = new ModuleMemberSyntaxNode(members[i].Attribute, members[i].Export, name, expression, members[i].Range);
					}
					else
					{
						array[num2++] = base.VisitModuleMember(members[i]);
					}
					hashSet.Remove(name);
				}
			}
			return array;
		}

		// Token: 0x0600A007 RID: 40967 RVA: 0x00210E5E File Offset: 0x0020F05E
		private IExpression InvalidFunction(Identifier identifier)
		{
			return this.InvalidExpression<Message1>(Strings.SemanticError_DuplicateParameter(identifier.Name));
		}

		// Token: 0x0600A008 RID: 40968 RVA: 0x00210E71 File Offset: 0x0020F071
		private IExpression InvalidLet(ILetExpression let)
		{
			return new LetExpressionSyntaxNode(this.RemoveDuplicateInitializers(let.Variables, new Func<object, Message1>(Strings.SemanticError_DuplicateInitializer)), base.VisitExpression(let.Expression), let.Range);
		}

		// Token: 0x0600A009 RID: 40969 RVA: 0x00210EA2 File Offset: 0x0020F0A2
		private IExpression InvalidRecord(Identifier identifier)
		{
			return this.InvalidExpression<Message1>(Strings.SemanticError_DuplicateField(identifier.Name));
		}

		// Token: 0x0600A00A RID: 40970 RVA: 0x00210EB5 File Offset: 0x0020F0B5
		private ISection InvalidModule(ISection module)
		{
			return new ModuleSyntaxNode(module.Attribute, module.SectionName, this.RemoveDuplicateModuleMembers(module.Members, new Func<object, Message1>(Strings.SemanticError_DuplicateInitializer)), module.Range);
		}

		// Token: 0x0600A00B RID: 40971 RVA: 0x00210EE8 File Offset: 0x0020F0E8
		protected override IExpression VisitFunction(IFunctionExpression function)
		{
			Identifier identifier;
			if (this.TryGetDuplicate(function.FunctionType.Parameters, out identifier))
			{
				this.log(SourceErrors.DuplicateParameter(this.GetLocation(identifier), identifier));
				return this.InvalidFunction(identifier);
			}
			return base.VisitFunction(function);
		}

		// Token: 0x0600A00C RID: 40972 RVA: 0x00210F34 File Offset: 0x0020F134
		protected override IExpression VisitFunctionType(IFunctionTypeExpression functionType)
		{
			Identifier identifier;
			if (this.TryGetDuplicate(functionType.Parameters, out identifier))
			{
				this.log(SourceErrors.DuplicateParameter(this.GetLocation(identifier), identifier));
				return this.InvalidFunction(identifier);
			}
			return base.VisitFunctionType(functionType);
		}

		// Token: 0x0600A00D RID: 40973 RVA: 0x00210F78 File Offset: 0x0020F178
		protected override IExpression VisitLet(ILetExpression let)
		{
			Identifier identifier;
			if (this.TryGetDuplicate(let.Variables, out identifier))
			{
				this.log(SourceErrors.DuplicateLocal(this.GetLocation(identifier), identifier));
				return this.InvalidLet(let);
			}
			return base.VisitLet(let);
		}

		// Token: 0x0600A00E RID: 40974 RVA: 0x00210FBC File Offset: 0x0020F1BC
		protected override IExpression VisitRecord(IRecordExpression record)
		{
			Identifier identifier;
			if (this.TryGetDuplicate(record.Members, out identifier))
			{
				this.log(SourceErrors.DuplicateField(this.GetLocation(identifier), identifier));
				return this.InvalidRecord(identifier);
			}
			return base.VisitRecord(record);
		}

		// Token: 0x0600A00F RID: 40975 RVA: 0x00211000 File Offset: 0x0020F200
		protected override IExpression VisitRecordType(IRecordTypeExpression recordType)
		{
			Identifier identifier;
			if (this.TryGetDuplicate(recordType.Fields, out identifier))
			{
				this.log(SourceErrors.DuplicateField(this.GetLocation(identifier), identifier));
				return this.InvalidRecord(identifier);
			}
			return base.VisitRecordType(recordType);
		}

		// Token: 0x0600A010 RID: 40976 RVA: 0x00211044 File Offset: 0x0020F244
		protected override ISection VisitModule(ISection module)
		{
			Identifier identifier;
			if (this.TryGetDuplicate(module.Members, out identifier))
			{
				SourceError sourceError = SourceErrors.DuplicateMember(this.GetLocation(identifier), identifier);
				this.log(sourceError);
				return this.InvalidModule(module);
			}
			return base.VisitModule(module);
		}

		// Token: 0x0600A011 RID: 40977 RVA: 0x0021108C File Offset: 0x0020F28C
		private SourceLocation GetLocation(ISyntaxNode node)
		{
			if (this.tokens != null)
			{
				return new SourceLocation(this.host, this.tokens.GetRange(node.Range.Start));
			}
			return SourceLocation.None;
		}

		// Token: 0x040053C6 RID: 21446
		private IDocumentHost host;

		// Token: 0x040053C7 RID: 21447
		private ITokens tokens;

		// Token: 0x040053C8 RID: 21448
		private Action<IError> log;
	}
}
