using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B88 RID: 7048
	public abstract class LogicalAstVisitor2<TBinding> : AstVisitor2
	{
		// Token: 0x0600B08E RID: 45198 RVA: 0x00242F4C File Offset: 0x0024114C
		protected LogicalAstVisitor2()
		{
			this.environment = new Environment<TBinding>();
		}

		// Token: 0x17002C14 RID: 11284
		// (get) Token: 0x0600B08F RID: 45199 RVA: 0x00242F5F File Offset: 0x0024115F
		protected Environment<TBinding> Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x0600B090 RID: 45200 RVA: 0x00242F67 File Offset: 0x00241167
		protected void EnterScope(TryCatchExceptionCase tryCatchCase, TBinding binding)
		{
			this.environment.EnterScope();
			this.environment.Add(tryCatchCase.Variable, binding);
		}

		// Token: 0x0600B091 RID: 45201 RVA: 0x00242F88 File Offset: 0x00241188
		protected void EnterScope(IFunctionExpression function, IList<TBinding> bindings)
		{
			IList<IParameter> parameters = function.FunctionType.Parameters;
			this.environment.EnterScope();
			for (int i = 0; i < parameters.Count; i++)
			{
				this.environment.Add(parameters[i].Identifier, bindings[i]);
			}
		}

		// Token: 0x0600B092 RID: 45202 RVA: 0x00242FDC File Offset: 0x002411DC
		protected void EnterScope(IList<VariableInitializer> members, IList<TBinding> bindings)
		{
			this.environment.EnterScope();
			for (int i = 0; i < members.Count; i++)
			{
				this.environment.Add(members[i].Name, bindings[i]);
			}
		}

		// Token: 0x0600B093 RID: 45203 RVA: 0x00243028 File Offset: 0x00241228
		protected void EnterScope(IList<ISectionMember> members, IList<TBinding> bindings)
		{
			this.environment.EnterScope();
			for (int i = 0; i < members.Count; i++)
			{
				this.environment.Add(members[i].Name, bindings[i]);
			}
		}

		// Token: 0x0600B094 RID: 45204 RVA: 0x0024306F File Offset: 0x0024126F
		protected void ExitScope(TryCatchExceptionCase tryCatchCase)
		{
			this.environment.Remove(tryCatchCase.Variable);
			this.environment.ExitScope();
		}

		// Token: 0x0600B095 RID: 45205 RVA: 0x00243090 File Offset: 0x00241290
		protected void ExitScope(IFunctionExpression function)
		{
			IList<IParameter> parameters = function.FunctionType.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				this.environment.Remove(parameters[i].Identifier);
			}
			this.environment.ExitScope();
		}

		// Token: 0x0600B096 RID: 45206 RVA: 0x002430DC File Offset: 0x002412DC
		protected void ExitScope(IList<VariableInitializer> members)
		{
			for (int i = 0; i < members.Count; i++)
			{
				this.environment.Remove(members[i].Name);
			}
			this.environment.ExitScope();
		}

		// Token: 0x0600B097 RID: 45207 RVA: 0x00243120 File Offset: 0x00241320
		protected void ExitScope(IList<ISectionMember> members)
		{
			for (int i = 0; i < members.Count; i++)
			{
				this.environment.Remove(members[i].Name);
			}
			this.environment.ExitScope();
		}

		// Token: 0x0600B098 RID: 45208
		protected abstract override IExpression VisitFunction(IFunctionExpression function);

		// Token: 0x0600B099 RID: 45209 RVA: 0x00243160 File Offset: 0x00241360
		protected IFunctionExpression VisitFunction(IFunctionExpression function, IList<TBinding> bindings)
		{
			IFunctionTypeExpression functionTypeExpression = this.VisitSignature(function.FunctionType);
			this.EnterScope(function, bindings);
			IExpression expression = this.VisitExpression(function.Expression);
			this.ExitScope(function);
			if (functionTypeExpression != function.FunctionType || expression != function.Expression)
			{
				function = new FunctionExpressionSyntaxNode(functionTypeExpression, expression, function.Range);
			}
			return function;
		}

		// Token: 0x0600B09A RID: 45210 RVA: 0x002431B8 File Offset: 0x002413B8
		protected override VariableInitializer VisitInitializer(VariableInitializer member)
		{
			this.environment.EnterExclusion(member.Name);
			VariableInitializer variableInitializer = new VariableInitializer(member.Name, this.VisitExpression(member.Value));
			this.environment.ExitExclusion(member.Name);
			return variableInitializer;
		}

		// Token: 0x0600B09B RID: 45211
		protected abstract override IExpression VisitLet(ILetExpression let);

		// Token: 0x0600B09C RID: 45212 RVA: 0x002431F8 File Offset: 0x002413F8
		protected IExpression VisitLet(ILetExpression let, IList<TBinding> bindings)
		{
			this.EnterScope(let.Variables, bindings);
			IList<VariableInitializer> list = base.VisitListElements(let.Variables);
			IExpression expression = this.VisitExpression(let.Expression);
			this.ExitScope(let.Variables);
			if (list != let.Variables || expression != let.Expression)
			{
				let = new LetExpressionSyntaxNode(list, expression, let.Range);
			}
			return let;
		}

		// Token: 0x0600B09D RID: 45213
		protected abstract override IExpression VisitRecord(IRecordExpression record);

		// Token: 0x0600B09E RID: 45214 RVA: 0x0024325C File Offset: 0x0024145C
		protected IRecordExpression VisitRecord(IRecordExpression record, TBinding binding, IList<TBinding> bindings)
		{
			this.EnterScope(record.Members, bindings);
			if (record.Identifier != null)
			{
				this.Environment.Add(record.Identifier, binding);
			}
			IList<VariableInitializer> list = base.VisitListElements(record.Members);
			if (record.Identifier != null)
			{
				this.Environment.Remove(record.Identifier);
			}
			this.ExitScope(record.Members);
			return base.CreateRecord(record, list);
		}

		// Token: 0x0600B09F RID: 45215
		protected abstract override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase);

		// Token: 0x0600B0A0 RID: 45216 RVA: 0x002432D8 File Offset: 0x002414D8
		protected TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase, TBinding value)
		{
			this.EnterScope(tryCatchExceptionCase, value);
			IExpression expression = this.VisitExpression(tryCatchExceptionCase.Expression);
			this.ExitScope(tryCatchExceptionCase);
			if (expression != tryCatchExceptionCase.Expression)
			{
				tryCatchExceptionCase = new TryCatchExceptionCase(tryCatchExceptionCase.Variable, expression);
			}
			return tryCatchExceptionCase;
		}

		// Token: 0x0600B0A1 RID: 45217
		protected abstract override ISection VisitModule(ISection module);

		// Token: 0x0600B0A2 RID: 45218 RVA: 0x0024331C File Offset: 0x0024151C
		protected ISection VisitModule(ISection module, IList<TBinding> bindings)
		{
			this.EnterScope(module.Members, bindings);
			IList<ISectionMember> list = base.VisitListElements(module.Members);
			this.ExitScope(module.Members);
			if (list != module.Members)
			{
				module = new ModuleSyntaxNode(module.Attribute, module.SectionName, list, module.Range);
			}
			return module;
		}

		// Token: 0x0600B0A3 RID: 45219 RVA: 0x00243374 File Offset: 0x00241574
		protected override ISectionMember VisitModuleMember(ISectionMember member)
		{
			this.environment.EnterExclusion(member.Name);
			IExpression expression = this.VisitExpression(member.Value);
			if (expression != member.Value)
			{
				member = new ModuleMemberSyntaxNode(member.Attribute, member.Export, member.Name, expression, member.Range);
			}
			this.environment.ExitExclusion(member.Name);
			return member;
		}

		// Token: 0x04005ACC RID: 23244
		private Environment<TBinding> environment;
	}
}
