using System;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001557 RID: 5463
	internal class LibraryHelper
	{
		// Token: 0x170023BE RID: 9150
		// (get) Token: 0x060087E4 RID: 34788 RVA: 0x001CD364 File Offset: 0x001CB564
		public static RecordValue StandardLibrary
		{
			get
			{
				if (LibraryHelper.standardLibrary == null)
				{
					LibraryHelper.standardLibrary = new LibraryHelper.SafeDelegatingRecordValue(Modules.GetLibrary(EngineHost.Empty, null));
				}
				return LibraryHelper.standardLibrary;
			}
		}

		// Token: 0x060087E5 RID: 34789 RVA: 0x001CD387 File Offset: 0x001CB587
		public static bool TryGetFunctionValue(string functionName, out FunctionValue functionValue)
		{
			return LibraryHelper.TryGetFunctionValue(LibraryHelper.StandardLibrary, functionName, out functionValue);
		}

		// Token: 0x060087E6 RID: 34790 RVA: 0x001CD395 File Offset: 0x001CB595
		public static bool TryGetFunctionValue(IEngineHost engineHost, string functionName, out FunctionValue functionValue)
		{
			return LibraryHelper.TryGetFunctionValue(LibraryHelper.GetLibrary(engineHost), functionName, out functionValue);
		}

		// Token: 0x060087E7 RID: 34791 RVA: 0x001CD3A4 File Offset: 0x001CB5A4
		public static bool TryGetFunctionValue(RecordValue library, string functionName, out FunctionValue functionValue)
		{
			Value value;
			if (library.TryGetValue(functionName, out value) && value.IsFunction)
			{
				functionValue = value.AsFunction;
				return true;
			}
			functionValue = null;
			return false;
		}

		// Token: 0x060087E8 RID: 34792 RVA: 0x001CD3D2 File Offset: 0x001CB5D2
		public static IExpression BindLibrary(IEngineHost engineHost, IExpression expression)
		{
			return new LibraryHelper.LibraryBinder(LibraryHelper.GetLibrary(engineHost)).Rewrite(expression);
		}

		// Token: 0x060087E9 RID: 34793 RVA: 0x001CD3E8 File Offset: 0x001CB5E8
		private static RecordValue GetLibrary(IEngineHost engineHost)
		{
			ICurrentEnvironmentService currentEnvironmentService = ((engineHost != null) ? engineHost.QueryService<ICurrentEnvironmentService>() : null);
			if (currentEnvironmentService != null)
			{
				return (RecordValue)currentEnvironmentService.Environment;
			}
			return LibraryHelper.StandardLibrary;
		}

		// Token: 0x04004B58 RID: 19288
		private static RecordValue standardLibrary;

		// Token: 0x02001558 RID: 5464
		private sealed class SafeDelegatingRecordValue : RecordValue
		{
			// Token: 0x060087EB RID: 34795 RVA: 0x001CD416 File Offset: 0x001CB616
			public SafeDelegatingRecordValue(RecordValue record)
			{
				this.syncRoot = new object();
				this.record = record;
			}

			// Token: 0x170023BF RID: 9151
			// (get) Token: 0x060087EC RID: 34796 RVA: 0x001CD430 File Offset: 0x001CB630
			public override Keys Keys
			{
				get
				{
					return this.record.Keys;
				}
			}

			// Token: 0x170023C0 RID: 9152
			// (get) Token: 0x060087ED RID: 34797 RVA: 0x001CD43D File Offset: 0x001CB63D
			public override TypeValue Type
			{
				get
				{
					return this.record.Type;
				}
			}

			// Token: 0x170023C1 RID: 9153
			public override Value this[int index]
			{
				get
				{
					return this.GetReference(index).Value;
				}
			}

			// Token: 0x060087EF RID: 34799 RVA: 0x001CD458 File Offset: 0x001CB658
			public override IValueReference GetReference(int index)
			{
				if (index >= this.Keys.Length || index < 0)
				{
					throw ValueException.RecordIndexOutOfRange(index, this);
				}
				object obj = this.syncRoot;
				IValueReference reference;
				lock (obj)
				{
					reference = this.record.GetReference(index);
				}
				return reference;
			}

			// Token: 0x04004B59 RID: 19289
			private readonly object syncRoot;

			// Token: 0x04004B5A RID: 19290
			private readonly RecordValue record;
		}

		// Token: 0x02001559 RID: 5465
		private sealed class LibraryBinder : LogicalAstVisitor<IExpression>
		{
			// Token: 0x060087F0 RID: 34800 RVA: 0x001CD4BC File Offset: 0x001CB6BC
			public LibraryBinder(RecordValue library)
			{
				this.library = library;
			}

			// Token: 0x060087F1 RID: 34801 RVA: 0x00146DFF File Offset: 0x00144FFF
			public IExpression Rewrite(IExpression expression)
			{
				return this.VisitExpression(expression);
			}

			// Token: 0x060087F2 RID: 34802 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override IExpression VisitFunction(IFunctionExpression function)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060087F3 RID: 34803 RVA: 0x001CD4CC File Offset: 0x001CB6CC
			protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
			{
				IExpression expression;
				FunctionValue functionValue;
				if (!base.Environment.TryGetValue(identifier.Name, identifier.IsInclusive, out expression) && LibraryHelper.TryGetFunctionValue(this.library, identifier.Name.Name, out functionValue))
				{
					return new ConstantExpressionSyntaxNode(functionValue);
				}
				return identifier;
			}

			// Token: 0x060087F4 RID: 34804 RVA: 0x001CD516 File Offset: 0x001CB716
			protected override IExpression VisitLet(ILetExpression let)
			{
				return base.VisitLet(let, new IExpression[let.Variables.Count]);
			}

			// Token: 0x060087F5 RID: 34805 RVA: 0x001CD52F File Offset: 0x001CB72F
			protected override ISection VisitModule(ISection module)
			{
				return base.VisitModule(module, new IExpression[module.Members.Count]);
			}

			// Token: 0x060087F6 RID: 34806 RVA: 0x001CD548 File Offset: 0x001CB748
			protected override IExpression VisitRecord(IRecordExpression record)
			{
				return base.VisitRecord(record, null, new IExpression[record.Members.Count]);
			}

			// Token: 0x060087F7 RID: 34807 RVA: 0x001CD562 File Offset: 0x001CB762
			protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
			{
				return base.VisitTryCatchExceptionCase(tryCatchExceptionCase, null);
			}

			// Token: 0x04004B5B RID: 19291
			private readonly RecordValue library;
		}
	}
}
