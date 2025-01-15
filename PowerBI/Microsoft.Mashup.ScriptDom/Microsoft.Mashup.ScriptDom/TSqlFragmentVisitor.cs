using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000CC RID: 204
	internal abstract class TSqlFragmentVisitor
	{
		// Token: 0x06000356 RID: 854 RVA: 0x00012D60 File Offset: 0x00010F60
		protected TSqlFragmentVisitor()
			: this(true)
		{
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00012D69 File Offset: 0x00010F69
		internal TSqlFragmentVisitor(bool visitBaseType)
		{
			this._visitBaseType = visitBaseType;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00012D78 File Offset: 0x00010F78
		internal bool VisitBaseType
		{
			get
			{
				return this._visitBaseType;
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00012D80 File Offset: 0x00010F80
		public virtual void Visit(TSqlFragment fragment)
		{
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00012D82 File Offset: 0x00010F82
		public virtual void Visit(StatementList node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00012D93 File Offset: 0x00010F93
		public virtual void ExplicitVisit(StatementList node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00012DB2 File Offset: 0x00010FB2
		public virtual void Visit(ExecuteStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00012DC3 File Offset: 0x00010FC3
		public virtual void ExplicitVisit(ExecuteStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00012DE9 File Offset: 0x00010FE9
		public virtual void Visit(ExecuteOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00012DFA File Offset: 0x00010FFA
		public virtual void ExplicitVisit(ExecuteOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00012E19 File Offset: 0x00011019
		public virtual void Visit(ResultSetsExecuteOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00012E2A File Offset: 0x0001102A
		public virtual void ExplicitVisit(ResultSetsExecuteOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00012E50 File Offset: 0x00011050
		public virtual void Visit(ResultSetDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00012E61 File Offset: 0x00011061
		public virtual void ExplicitVisit(ResultSetDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00012E80 File Offset: 0x00011080
		public virtual void Visit(InlineResultSetDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00012E91 File Offset: 0x00011091
		public virtual void ExplicitVisit(InlineResultSetDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00012EB7 File Offset: 0x000110B7
		public virtual void Visit(ResultColumnDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00012EC8 File Offset: 0x000110C8
		public virtual void ExplicitVisit(ResultColumnDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00012EE7 File Offset: 0x000110E7
		public virtual void Visit(SchemaObjectResultSetDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00012EF8 File Offset: 0x000110F8
		public virtual void ExplicitVisit(SchemaObjectResultSetDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00012F1E File Offset: 0x0001111E
		public virtual void Visit(ExecuteSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00012F2F File Offset: 0x0001112F
		public virtual void ExplicitVisit(ExecuteSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00012F4E File Offset: 0x0001114E
		public virtual void Visit(ExecuteContext node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00012F5F File Offset: 0x0001115F
		public virtual void ExplicitVisit(ExecuteContext node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00012F7E File Offset: 0x0001117E
		public virtual void Visit(ExecuteParameter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00012F8F File Offset: 0x0001118F
		public virtual void ExplicitVisit(ExecuteParameter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00012FAE File Offset: 0x000111AE
		public virtual void Visit(ExecutableEntity node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00012FBF File Offset: 0x000111BF
		public virtual void ExplicitVisit(ExecutableEntity node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00012FDE File Offset: 0x000111DE
		public virtual void Visit(ProcedureReferenceName node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00012FEF File Offset: 0x000111EF
		public virtual void ExplicitVisit(ProcedureReferenceName node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001300E File Offset: 0x0001120E
		public virtual void Visit(ExecutableProcedureReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001301F File Offset: 0x0001121F
		public virtual void ExplicitVisit(ExecutableProcedureReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00013045 File Offset: 0x00011245
		public virtual void Visit(ExecutableStringList node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00013056 File Offset: 0x00011256
		public virtual void ExplicitVisit(ExecutableStringList node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001307C File Offset: 0x0001127C
		public virtual void Visit(AdHocDataSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001308D File Offset: 0x0001128D
		public virtual void ExplicitVisit(AdHocDataSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000130AC File Offset: 0x000112AC
		public virtual void Visit(ViewOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000130BD File Offset: 0x000112BD
		public virtual void ExplicitVisit(ViewOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000130DC File Offset: 0x000112DC
		public virtual void Visit(AlterViewStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000130ED File Offset: 0x000112ED
		public virtual void ExplicitVisit(AlterViewStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001311A File Offset: 0x0001131A
		public virtual void Visit(CreateViewStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001312B File Offset: 0x0001132B
		public virtual void ExplicitVisit(CreateViewStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00013158 File Offset: 0x00011358
		public virtual void Visit(ViewStatementBody node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00013169 File Offset: 0x00011369
		public virtual void ExplicitVisit(ViewStatementBody node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001318F File Offset: 0x0001138F
		public virtual void Visit(TriggerObject node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000131A0 File Offset: 0x000113A0
		public virtual void ExplicitVisit(TriggerObject node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000131BF File Offset: 0x000113BF
		public virtual void Visit(TriggerOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000131D0 File Offset: 0x000113D0
		public virtual void ExplicitVisit(TriggerOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000131EF File Offset: 0x000113EF
		public virtual void Visit(ExecuteAsTriggerOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00013200 File Offset: 0x00011400
		public virtual void ExplicitVisit(ExecuteAsTriggerOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00013226 File Offset: 0x00011426
		public virtual void Visit(TriggerAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00013237 File Offset: 0x00011437
		public virtual void ExplicitVisit(TriggerAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00013256 File Offset: 0x00011456
		public virtual void Visit(AlterTriggerStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00013267 File Offset: 0x00011467
		public virtual void ExplicitVisit(AlterTriggerStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00013294 File Offset: 0x00011494
		public virtual void Visit(CreateTriggerStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000132A5 File Offset: 0x000114A5
		public virtual void ExplicitVisit(CreateTriggerStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000132D2 File Offset: 0x000114D2
		public virtual void Visit(TriggerStatementBody node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000132E3 File Offset: 0x000114E3
		public virtual void ExplicitVisit(TriggerStatementBody node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00013309 File Offset: 0x00011509
		public virtual void Visit(Identifier node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001331A File Offset: 0x0001151A
		public virtual void ExplicitVisit(Identifier node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00013339 File Offset: 0x00011539
		public virtual void Visit(AlterProcedureStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001334A File Offset: 0x0001154A
		public virtual void ExplicitVisit(AlterProcedureStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001337E File Offset: 0x0001157E
		public virtual void Visit(CreateProcedureStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001338F File Offset: 0x0001158F
		public virtual void ExplicitVisit(CreateProcedureStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000133C3 File Offset: 0x000115C3
		public virtual void Visit(ProcedureReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000133D4 File Offset: 0x000115D4
		public virtual void ExplicitVisit(ProcedureReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000133F3 File Offset: 0x000115F3
		public virtual void Visit(MethodSpecifier node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00013404 File Offset: 0x00011604
		public virtual void ExplicitVisit(MethodSpecifier node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00013423 File Offset: 0x00011623
		public virtual void Visit(ProcedureStatementBody node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00013434 File Offset: 0x00011634
		public virtual void ExplicitVisit(ProcedureStatementBody node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00013461 File Offset: 0x00011661
		public virtual void Visit(ProcedureStatementBodyBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00013472 File Offset: 0x00011672
		public virtual void ExplicitVisit(ProcedureStatementBodyBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00013498 File Offset: 0x00011698
		public virtual void Visit(FunctionStatementBody node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000134A9 File Offset: 0x000116A9
		public virtual void ExplicitVisit(FunctionStatementBody node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000134D6 File Offset: 0x000116D6
		public virtual void Visit(ProcedureOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000134E7 File Offset: 0x000116E7
		public virtual void ExplicitVisit(ProcedureOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00013506 File Offset: 0x00011706
		public virtual void Visit(ExecuteAsProcedureOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00013517 File Offset: 0x00011717
		public virtual void ExplicitVisit(ExecuteAsProcedureOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001353D File Offset: 0x0001173D
		public virtual void Visit(FunctionOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001354E File Offset: 0x0001174E
		public virtual void ExplicitVisit(FunctionOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001356D File Offset: 0x0001176D
		public virtual void Visit(ExecuteAsFunctionOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001357E File Offset: 0x0001177E
		public virtual void ExplicitVisit(ExecuteAsFunctionOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000135A4 File Offset: 0x000117A4
		public virtual void Visit(XmlNamespaces node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000135B5 File Offset: 0x000117B5
		public virtual void ExplicitVisit(XmlNamespaces node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000135D4 File Offset: 0x000117D4
		public virtual void Visit(XmlNamespacesElement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000135E5 File Offset: 0x000117E5
		public virtual void ExplicitVisit(XmlNamespacesElement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00013604 File Offset: 0x00011804
		public virtual void Visit(XmlNamespacesDefaultElement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00013615 File Offset: 0x00011815
		public virtual void ExplicitVisit(XmlNamespacesDefaultElement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001363B File Offset: 0x0001183B
		public virtual void Visit(XmlNamespacesAliasElement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001364C File Offset: 0x0001184C
		public virtual void ExplicitVisit(XmlNamespacesAliasElement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00013672 File Offset: 0x00011872
		public virtual void Visit(CommonTableExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00013683 File Offset: 0x00011883
		public virtual void ExplicitVisit(CommonTableExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000136A2 File Offset: 0x000118A2
		public virtual void Visit(WithCtesAndXmlNamespaces node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000136B3 File Offset: 0x000118B3
		public virtual void ExplicitVisit(WithCtesAndXmlNamespaces node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000136D2 File Offset: 0x000118D2
		public virtual void Visit(FunctionReturnType node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000136E3 File Offset: 0x000118E3
		public virtual void ExplicitVisit(FunctionReturnType node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00013702 File Offset: 0x00011902
		public virtual void Visit(TableValuedFunctionReturnType node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00013713 File Offset: 0x00011913
		public virtual void ExplicitVisit(TableValuedFunctionReturnType node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00013739 File Offset: 0x00011939
		public virtual void Visit(DataTypeReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001374A File Offset: 0x0001194A
		public virtual void ExplicitVisit(DataTypeReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00013769 File Offset: 0x00011969
		public virtual void Visit(ParameterizedDataTypeReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001377A File Offset: 0x0001197A
		public virtual void ExplicitVisit(ParameterizedDataTypeReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000137A0 File Offset: 0x000119A0
		public virtual void Visit(SqlDataTypeReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000137B1 File Offset: 0x000119B1
		public virtual void ExplicitVisit(SqlDataTypeReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000137DE File Offset: 0x000119DE
		public virtual void Visit(UserDataTypeReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000137EF File Offset: 0x000119EF
		public virtual void ExplicitVisit(UserDataTypeReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001381C File Offset: 0x00011A1C
		public virtual void Visit(XmlDataTypeReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001382D File Offset: 0x00011A2D
		public virtual void ExplicitVisit(XmlDataTypeReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00013853 File Offset: 0x00011A53
		public virtual void Visit(ScalarFunctionReturnType node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00013864 File Offset: 0x00011A64
		public virtual void ExplicitVisit(ScalarFunctionReturnType node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001388A File Offset: 0x00011A8A
		public virtual void Visit(SelectFunctionReturnType node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001389B File Offset: 0x00011A9B
		public virtual void ExplicitVisit(SelectFunctionReturnType node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000138C1 File Offset: 0x00011AC1
		public virtual void Visit(TableDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000138D2 File Offset: 0x00011AD2
		public virtual void ExplicitVisit(TableDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000138F1 File Offset: 0x00011AF1
		public virtual void Visit(DeclareTableVariableBody node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00013902 File Offset: 0x00011B02
		public virtual void ExplicitVisit(DeclareTableVariableBody node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00013921 File Offset: 0x00011B21
		public virtual void Visit(DeclareTableVariableStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00013932 File Offset: 0x00011B32
		public virtual void ExplicitVisit(DeclareTableVariableStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00013958 File Offset: 0x00011B58
		public virtual void Visit(NamedTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00013969 File Offset: 0x00011B69
		public virtual void ExplicitVisit(NamedTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00013996 File Offset: 0x00011B96
		public virtual void Visit(SchemaObjectFunctionTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000139A7 File Offset: 0x00011BA7
		public virtual void ExplicitVisit(SchemaObjectFunctionTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000139DB File Offset: 0x00011BDB
		public virtual void Visit(TableHint node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000139EC File Offset: 0x00011BEC
		public virtual void ExplicitVisit(TableHint node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00013A0B File Offset: 0x00011C0B
		public virtual void Visit(IndexTableHint node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00013A1C File Offset: 0x00011C1C
		public virtual void ExplicitVisit(IndexTableHint node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00013A42 File Offset: 0x00011C42
		public virtual void Visit(LiteralTableHint node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00013A53 File Offset: 0x00011C53
		public virtual void ExplicitVisit(LiteralTableHint node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00013A79 File Offset: 0x00011C79
		public virtual void Visit(QueryDerivedTable node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00013A8A File Offset: 0x00011C8A
		public virtual void ExplicitVisit(QueryDerivedTable node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00013ABE File Offset: 0x00011CBE
		public virtual void Visit(InlineDerivedTable node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00013ACF File Offset: 0x00011CCF
		public virtual void ExplicitVisit(InlineDerivedTable node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00013B03 File Offset: 0x00011D03
		public virtual void Visit(SubqueryComparisonPredicate node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00013B14 File Offset: 0x00011D14
		public virtual void ExplicitVisit(SubqueryComparisonPredicate node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00013B3A File Offset: 0x00011D3A
		public virtual void Visit(ExistsPredicate node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00013B4B File Offset: 0x00011D4B
		public virtual void ExplicitVisit(ExistsPredicate node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00013B71 File Offset: 0x00011D71
		public virtual void Visit(LikePredicate node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00013B82 File Offset: 0x00011D82
		public virtual void ExplicitVisit(LikePredicate node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00013BA8 File Offset: 0x00011DA8
		public virtual void Visit(InPredicate node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00013BB9 File Offset: 0x00011DB9
		public virtual void ExplicitVisit(InPredicate node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00013BDF File Offset: 0x00011DDF
		public virtual void Visit(FullTextPredicate node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public virtual void ExplicitVisit(FullTextPredicate node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00013C16 File Offset: 0x00011E16
		public virtual void Visit(UserDefinedTypePropertyAccess node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00013C27 File Offset: 0x00011E27
		public virtual void ExplicitVisit(UserDefinedTypePropertyAccess node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00013C54 File Offset: 0x00011E54
		public virtual void Visit(StatementWithCtesAndXmlNamespaces node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00013C65 File Offset: 0x00011E65
		public virtual void ExplicitVisit(StatementWithCtesAndXmlNamespaces node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00013C8B File Offset: 0x00011E8B
		public virtual void Visit(SelectStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00013C9C File Offset: 0x00011E9C
		public virtual void ExplicitVisit(SelectStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00013CC9 File Offset: 0x00011EC9
		public virtual void Visit(ForClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00013CDA File Offset: 0x00011EDA
		public virtual void ExplicitVisit(ForClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00013CF9 File Offset: 0x00011EF9
		public virtual void Visit(BrowseForClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00013D0A File Offset: 0x00011F0A
		public virtual void ExplicitVisit(BrowseForClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00013D30 File Offset: 0x00011F30
		public virtual void Visit(ReadOnlyForClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00013D41 File Offset: 0x00011F41
		public virtual void ExplicitVisit(ReadOnlyForClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00013D67 File Offset: 0x00011F67
		public virtual void Visit(XmlForClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00013D78 File Offset: 0x00011F78
		public virtual void ExplicitVisit(XmlForClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013D9E File Offset: 0x00011F9E
		public virtual void Visit(XmlForClauseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00013DAF File Offset: 0x00011FAF
		public virtual void ExplicitVisit(XmlForClauseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013DD5 File Offset: 0x00011FD5
		public virtual void Visit(UpdateForClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00013DE6 File Offset: 0x00011FE6
		public virtual void ExplicitVisit(UpdateForClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00013E0C File Offset: 0x0001200C
		public virtual void Visit(OptimizerHint node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013E1D File Offset: 0x0001201D
		public virtual void ExplicitVisit(OptimizerHint node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00013E3C File Offset: 0x0001203C
		public virtual void Visit(LiteralOptimizerHint node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00013E4D File Offset: 0x0001204D
		public virtual void ExplicitVisit(LiteralOptimizerHint node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00013E73 File Offset: 0x00012073
		public virtual void Visit(TableHintsOptimizerHint node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00013E84 File Offset: 0x00012084
		public virtual void ExplicitVisit(TableHintsOptimizerHint node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013EAA File Offset: 0x000120AA
		public virtual void Visit(ForceSeekTableHint node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00013EBB File Offset: 0x000120BB
		public virtual void ExplicitVisit(ForceSeekTableHint node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00013EE1 File Offset: 0x000120E1
		public virtual void Visit(OptimizeForOptimizerHint node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00013EF2 File Offset: 0x000120F2
		public virtual void ExplicitVisit(OptimizeForOptimizerHint node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00013F18 File Offset: 0x00012118
		public virtual void Visit(VariableValuePair node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00013F29 File Offset: 0x00012129
		public virtual void ExplicitVisit(VariableValuePair node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00013F48 File Offset: 0x00012148
		public virtual void Visit(WhenClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00013F59 File Offset: 0x00012159
		public virtual void ExplicitVisit(WhenClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00013F78 File Offset: 0x00012178
		public virtual void Visit(SimpleWhenClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00013F89 File Offset: 0x00012189
		public virtual void ExplicitVisit(SimpleWhenClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00013FAF File Offset: 0x000121AF
		public virtual void Visit(SearchedWhenClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00013FC0 File Offset: 0x000121C0
		public virtual void ExplicitVisit(SearchedWhenClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00013FE6 File Offset: 0x000121E6
		public virtual void Visit(CaseExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00013FF7 File Offset: 0x000121F7
		public virtual void ExplicitVisit(CaseExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00014024 File Offset: 0x00012224
		public virtual void Visit(SimpleCaseExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00014035 File Offset: 0x00012235
		public virtual void ExplicitVisit(SimpleCaseExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00014069 File Offset: 0x00012269
		public virtual void Visit(SearchedCaseExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001407A File Offset: 0x0001227A
		public virtual void ExplicitVisit(SearchedCaseExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000140AE File Offset: 0x000122AE
		public virtual void Visit(NullIfExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000140BF File Offset: 0x000122BF
		public virtual void ExplicitVisit(NullIfExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000140EC File Offset: 0x000122EC
		public virtual void Visit(CoalesceExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000140FD File Offset: 0x000122FD
		public virtual void ExplicitVisit(CoalesceExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001412A File Offset: 0x0001232A
		public virtual void Visit(IIfCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001413B File Offset: 0x0001233B
		public virtual void ExplicitVisit(IIfCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00014168 File Offset: 0x00012368
		public virtual void Visit(FullTextTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00014179 File Offset: 0x00012379
		public virtual void ExplicitVisit(FullTextTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000141A6 File Offset: 0x000123A6
		public virtual void Visit(SemanticTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000141B7 File Offset: 0x000123B7
		public virtual void ExplicitVisit(SemanticTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000141E4 File Offset: 0x000123E4
		public virtual void Visit(OpenXmlTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000141F5 File Offset: 0x000123F5
		public virtual void ExplicitVisit(OpenXmlTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00014222 File Offset: 0x00012422
		public virtual void Visit(OpenRowsetTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00014233 File Offset: 0x00012433
		public virtual void ExplicitVisit(OpenRowsetTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00014260 File Offset: 0x00012460
		public virtual void Visit(InternalOpenRowset node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00014271 File Offset: 0x00012471
		public virtual void ExplicitVisit(InternalOpenRowset node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001429E File Offset: 0x0001249E
		public virtual void Visit(BulkOpenRowset node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000142AF File Offset: 0x000124AF
		public virtual void ExplicitVisit(BulkOpenRowset node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000142E3 File Offset: 0x000124E3
		public virtual void Visit(OpenQueryTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000142F4 File Offset: 0x000124F4
		public virtual void ExplicitVisit(OpenQueryTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00014321 File Offset: 0x00012521
		public virtual void Visit(AdHocTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00014332 File Offset: 0x00012532
		public virtual void ExplicitVisit(AdHocTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001435F File Offset: 0x0001255F
		public virtual void Visit(SchemaDeclarationItem node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00014370 File Offset: 0x00012570
		public virtual void ExplicitVisit(SchemaDeclarationItem node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001438F File Offset: 0x0001258F
		public virtual void Visit(ConvertCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000143A0 File Offset: 0x000125A0
		public virtual void ExplicitVisit(ConvertCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000143CD File Offset: 0x000125CD
		public virtual void Visit(TryConvertCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000143DE File Offset: 0x000125DE
		public virtual void ExplicitVisit(TryConvertCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001440B File Offset: 0x0001260B
		public virtual void Visit(ParseCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001441C File Offset: 0x0001261C
		public virtual void ExplicitVisit(ParseCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00014449 File Offset: 0x00012649
		public virtual void Visit(TryParseCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001445A File Offset: 0x0001265A
		public virtual void ExplicitVisit(TryParseCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00014487 File Offset: 0x00012687
		public virtual void Visit(CastCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00014498 File Offset: 0x00012698
		public virtual void ExplicitVisit(CastCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000144C5 File Offset: 0x000126C5
		public virtual void Visit(TryCastCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000144D6 File Offset: 0x000126D6
		public virtual void ExplicitVisit(TryCastCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00014503 File Offset: 0x00012703
		public virtual void Visit(FunctionCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00014514 File Offset: 0x00012714
		public virtual void ExplicitVisit(FunctionCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00014541 File Offset: 0x00012741
		public virtual void Visit(CallTarget node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00014552 File Offset: 0x00012752
		public virtual void ExplicitVisit(CallTarget node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00014571 File Offset: 0x00012771
		public virtual void Visit(ExpressionCallTarget node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00014582 File Offset: 0x00012782
		public virtual void ExplicitVisit(ExpressionCallTarget node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000145A8 File Offset: 0x000127A8
		public virtual void Visit(MultiPartIdentifierCallTarget node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000145B9 File Offset: 0x000127B9
		public virtual void ExplicitVisit(MultiPartIdentifierCallTarget node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000145DF File Offset: 0x000127DF
		public virtual void Visit(UserDefinedTypeCallTarget node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000145F0 File Offset: 0x000127F0
		public virtual void ExplicitVisit(UserDefinedTypeCallTarget node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00014616 File Offset: 0x00012816
		public virtual void Visit(LeftFunctionCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00014627 File Offset: 0x00012827
		public virtual void ExplicitVisit(LeftFunctionCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00014654 File Offset: 0x00012854
		public virtual void Visit(RightFunctionCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00014665 File Offset: 0x00012865
		public virtual void ExplicitVisit(RightFunctionCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00014692 File Offset: 0x00012892
		public virtual void Visit(PartitionFunctionCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000146A3 File Offset: 0x000128A3
		public virtual void ExplicitVisit(PartitionFunctionCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000146D0 File Offset: 0x000128D0
		public virtual void Visit(OverClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000146E1 File Offset: 0x000128E1
		public virtual void ExplicitVisit(OverClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00014700 File Offset: 0x00012900
		public virtual void Visit(ParameterlessCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00014711 File Offset: 0x00012911
		public virtual void ExplicitVisit(ParameterlessCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001473E File Offset: 0x0001293E
		public virtual void Visit(ScalarSubquery node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001474F File Offset: 0x0001294F
		public virtual void ExplicitVisit(ScalarSubquery node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001477C File Offset: 0x0001297C
		public virtual void Visit(OdbcFunctionCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001478D File Offset: 0x0001298D
		public virtual void ExplicitVisit(OdbcFunctionCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000147BA File Offset: 0x000129BA
		public virtual void Visit(ExtractFromExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000147CB File Offset: 0x000129CB
		public virtual void ExplicitVisit(ExtractFromExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000147F1 File Offset: 0x000129F1
		public virtual void Visit(OdbcConvertSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00014802 File Offset: 0x00012A02
		public virtual void ExplicitVisit(OdbcConvertSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00014828 File Offset: 0x00012A28
		public virtual void Visit(AlterFunctionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00014839 File Offset: 0x00012A39
		public virtual void ExplicitVisit(AlterFunctionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001486D File Offset: 0x00012A6D
		public virtual void Visit(BeginEndBlockStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001487E File Offset: 0x00012A7E
		public virtual void ExplicitVisit(BeginEndBlockStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000148A4 File Offset: 0x00012AA4
		public virtual void Visit(BeginTransactionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000148B5 File Offset: 0x00012AB5
		public virtual void ExplicitVisit(BeginTransactionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000148E2 File Offset: 0x00012AE2
		public virtual void Visit(BreakStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000148F3 File Offset: 0x00012AF3
		public virtual void ExplicitVisit(BreakStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00014919 File Offset: 0x00012B19
		public virtual void Visit(ColumnWithSortOrder node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001492A File Offset: 0x00012B2A
		public virtual void ExplicitVisit(ColumnWithSortOrder node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00014949 File Offset: 0x00012B49
		public virtual void Visit(CommitTransactionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001495A File Offset: 0x00012B5A
		public virtual void ExplicitVisit(CommitTransactionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00014987 File Offset: 0x00012B87
		public virtual void Visit(RollbackTransactionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00014998 File Offset: 0x00012B98
		public virtual void ExplicitVisit(RollbackTransactionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000149C5 File Offset: 0x00012BC5
		public virtual void Visit(SaveTransactionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000149D6 File Offset: 0x00012BD6
		public virtual void ExplicitVisit(SaveTransactionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00014A03 File Offset: 0x00012C03
		public virtual void Visit(ContinueStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00014A14 File Offset: 0x00012C14
		public virtual void ExplicitVisit(ContinueStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00014A3A File Offset: 0x00012C3A
		public virtual void Visit(CreateDefaultStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00014A4B File Offset: 0x00012C4B
		public virtual void ExplicitVisit(CreateDefaultStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00014A71 File Offset: 0x00012C71
		public virtual void Visit(CreateFunctionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00014A82 File Offset: 0x00012C82
		public virtual void ExplicitVisit(CreateFunctionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00014AB6 File Offset: 0x00012CB6
		public virtual void Visit(CreateRuleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00014AC7 File Offset: 0x00012CC7
		public virtual void ExplicitVisit(CreateRuleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00014AED File Offset: 0x00012CED
		public virtual void Visit(DeclareVariableElement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00014AFE File Offset: 0x00012CFE
		public virtual void ExplicitVisit(DeclareVariableElement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00014B1D File Offset: 0x00012D1D
		public virtual void Visit(DeclareVariableStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00014B2E File Offset: 0x00012D2E
		public virtual void ExplicitVisit(DeclareVariableStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00014B54 File Offset: 0x00012D54
		public virtual void Visit(GoToStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00014B65 File Offset: 0x00012D65
		public virtual void ExplicitVisit(GoToStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00014B8B File Offset: 0x00012D8B
		public virtual void Visit(IfStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00014B9C File Offset: 0x00012D9C
		public virtual void ExplicitVisit(IfStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00014BC2 File Offset: 0x00012DC2
		public virtual void Visit(LabelStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00014BD3 File Offset: 0x00012DD3
		public virtual void ExplicitVisit(LabelStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00014BF9 File Offset: 0x00012DF9
		public virtual void Visit(MultiPartIdentifier node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00014C0A File Offset: 0x00012E0A
		public virtual void ExplicitVisit(MultiPartIdentifier node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00014C29 File Offset: 0x00012E29
		public virtual void Visit(SchemaObjectName node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00014C3A File Offset: 0x00012E3A
		public virtual void ExplicitVisit(SchemaObjectName node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00014C60 File Offset: 0x00012E60
		public virtual void Visit(ChildObjectName node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00014C71 File Offset: 0x00012E71
		public virtual void ExplicitVisit(ChildObjectName node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00014C9E File Offset: 0x00012E9E
		public virtual void Visit(ProcedureParameter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00014CAF File Offset: 0x00012EAF
		public virtual void ExplicitVisit(ProcedureParameter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00014CD5 File Offset: 0x00012ED5
		public virtual void Visit(TransactionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00014CE6 File Offset: 0x00012EE6
		public virtual void ExplicitVisit(TransactionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00014D0C File Offset: 0x00012F0C
		public virtual void Visit(WhileStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014D1D File Offset: 0x00012F1D
		public virtual void ExplicitVisit(WhileStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00014D43 File Offset: 0x00012F43
		public virtual void Visit(DeleteStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00014D54 File Offset: 0x00012F54
		public virtual void ExplicitVisit(DeleteStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00014D88 File Offset: 0x00012F88
		public virtual void Visit(UpdateDeleteSpecificationBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00014D99 File Offset: 0x00012F99
		public virtual void ExplicitVisit(UpdateDeleteSpecificationBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00014DBF File Offset: 0x00012FBF
		public virtual void Visit(DeleteSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00014DD0 File Offset: 0x00012FD0
		public virtual void ExplicitVisit(DeleteSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00014DFD File Offset: 0x00012FFD
		public virtual void Visit(InsertStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00014E0E File Offset: 0x0001300E
		public virtual void ExplicitVisit(InsertStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00014E42 File Offset: 0x00013042
		public virtual void Visit(InsertSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00014E53 File Offset: 0x00013053
		public virtual void ExplicitVisit(InsertSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00014E79 File Offset: 0x00013079
		public virtual void Visit(UpdateStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00014E8A File Offset: 0x0001308A
		public virtual void ExplicitVisit(UpdateStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00014EBE File Offset: 0x000130BE
		public virtual void Visit(UpdateSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00014ECF File Offset: 0x000130CF
		public virtual void ExplicitVisit(UpdateSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00014EFC File Offset: 0x000130FC
		public virtual void Visit(CreateSchemaStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00014F0D File Offset: 0x0001310D
		public virtual void ExplicitVisit(CreateSchemaStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00014F33 File Offset: 0x00013133
		public virtual void Visit(WaitForStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00014F44 File Offset: 0x00013144
		public virtual void ExplicitVisit(WaitForStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00014F6A File Offset: 0x0001316A
		public virtual void Visit(ReadTextStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00014F7B File Offset: 0x0001317B
		public virtual void ExplicitVisit(ReadTextStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00014FA1 File Offset: 0x000131A1
		public virtual void Visit(UpdateTextStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00014FB2 File Offset: 0x000131B2
		public virtual void ExplicitVisit(UpdateTextStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00014FDF File Offset: 0x000131DF
		public virtual void Visit(WriteTextStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00014FF0 File Offset: 0x000131F0
		public virtual void ExplicitVisit(WriteTextStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001501D File Offset: 0x0001321D
		public virtual void Visit(TextModificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001502E File Offset: 0x0001322E
		public virtual void ExplicitVisit(TextModificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00015054 File Offset: 0x00013254
		public virtual void Visit(LineNoStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00015065 File Offset: 0x00013265
		public virtual void ExplicitVisit(LineNoStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001508B File Offset: 0x0001328B
		public virtual void Visit(SecurityStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001509C File Offset: 0x0001329C
		public virtual void ExplicitVisit(SecurityStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000150C2 File Offset: 0x000132C2
		public virtual void Visit(GrantStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000150D3 File Offset: 0x000132D3
		public virtual void ExplicitVisit(GrantStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00015100 File Offset: 0x00013300
		public virtual void Visit(DenyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00015111 File Offset: 0x00013311
		public virtual void ExplicitVisit(DenyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001513E File Offset: 0x0001333E
		public virtual void Visit(RevokeStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001514F File Offset: 0x0001334F
		public virtual void ExplicitVisit(RevokeStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001517C File Offset: 0x0001337C
		public virtual void Visit(AlterAuthorizationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001518D File Offset: 0x0001338D
		public virtual void ExplicitVisit(AlterAuthorizationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000151B3 File Offset: 0x000133B3
		public virtual void Visit(Permission node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000151C4 File Offset: 0x000133C4
		public virtual void ExplicitVisit(Permission node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000151E3 File Offset: 0x000133E3
		public virtual void Visit(SecurityTargetObject node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000151F4 File Offset: 0x000133F4
		public virtual void ExplicitVisit(SecurityTargetObject node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00015213 File Offset: 0x00013413
		public virtual void Visit(SecurityTargetObjectName node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00015224 File Offset: 0x00013424
		public virtual void ExplicitVisit(SecurityTargetObjectName node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00015243 File Offset: 0x00013443
		public virtual void Visit(SecurityPrincipal node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00015254 File Offset: 0x00013454
		public virtual void ExplicitVisit(SecurityPrincipal node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00015273 File Offset: 0x00013473
		public virtual void Visit(SecurityStatementBody80 node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00015284 File Offset: 0x00013484
		public virtual void ExplicitVisit(SecurityStatementBody80 node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000152AA File Offset: 0x000134AA
		public virtual void Visit(GrantStatement80 node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000152BB File Offset: 0x000134BB
		public virtual void ExplicitVisit(GrantStatement80 node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000152E8 File Offset: 0x000134E8
		public virtual void Visit(DenyStatement80 node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000152F9 File Offset: 0x000134F9
		public virtual void ExplicitVisit(DenyStatement80 node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00015326 File Offset: 0x00013526
		public virtual void Visit(RevokeStatement80 node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00015337 File Offset: 0x00013537
		public virtual void ExplicitVisit(RevokeStatement80 node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00015364 File Offset: 0x00013564
		public virtual void Visit(SecurityElement80 node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00015375 File Offset: 0x00013575
		public virtual void ExplicitVisit(SecurityElement80 node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00015394 File Offset: 0x00013594
		public virtual void Visit(CommandSecurityElement80 node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000153A5 File Offset: 0x000135A5
		public virtual void ExplicitVisit(CommandSecurityElement80 node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000153CB File Offset: 0x000135CB
		public virtual void Visit(PrivilegeSecurityElement80 node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000153DC File Offset: 0x000135DC
		public virtual void ExplicitVisit(PrivilegeSecurityElement80 node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00015402 File Offset: 0x00013602
		public virtual void Visit(Privilege80 node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00015413 File Offset: 0x00013613
		public virtual void ExplicitVisit(Privilege80 node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00015432 File Offset: 0x00013632
		public virtual void Visit(SecurityUserClause80 node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00015443 File Offset: 0x00013643
		public virtual void ExplicitVisit(SecurityUserClause80 node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00015462 File Offset: 0x00013662
		public virtual void Visit(SqlCommandIdentifier node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00015473 File Offset: 0x00013673
		public virtual void ExplicitVisit(SqlCommandIdentifier node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00015499 File Offset: 0x00013699
		public virtual void Visit(SetClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000154AA File Offset: 0x000136AA
		public virtual void ExplicitVisit(SetClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000154C9 File Offset: 0x000136C9
		public virtual void Visit(AssignmentSetClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000154DA File Offset: 0x000136DA
		public virtual void ExplicitVisit(AssignmentSetClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00015500 File Offset: 0x00013700
		public virtual void Visit(FunctionCallSetClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00015511 File Offset: 0x00013711
		public virtual void ExplicitVisit(FunctionCallSetClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00015537 File Offset: 0x00013737
		public virtual void Visit(InsertSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00015548 File Offset: 0x00013748
		public virtual void ExplicitVisit(InsertSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00015567 File Offset: 0x00013767
		public virtual void Visit(ValuesInsertSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00015578 File Offset: 0x00013778
		public virtual void ExplicitVisit(ValuesInsertSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001559E File Offset: 0x0001379E
		public virtual void Visit(SelectInsertSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000155AF File Offset: 0x000137AF
		public virtual void ExplicitVisit(SelectInsertSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000155D5 File Offset: 0x000137D5
		public virtual void Visit(ExecuteInsertSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000155E6 File Offset: 0x000137E6
		public virtual void ExplicitVisit(ExecuteInsertSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001560C File Offset: 0x0001380C
		public virtual void Visit(RowValue node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001561D File Offset: 0x0001381D
		public virtual void ExplicitVisit(RowValue node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001563C File Offset: 0x0001383C
		public virtual void Visit(PrintStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001564D File Offset: 0x0001384D
		public virtual void ExplicitVisit(PrintStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00015673 File Offset: 0x00013873
		public virtual void Visit(UpdateCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00015684 File Offset: 0x00013884
		public virtual void ExplicitVisit(UpdateCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000156AA File Offset: 0x000138AA
		public virtual void Visit(TSEqualCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000156BB File Offset: 0x000138BB
		public virtual void ExplicitVisit(TSEqualCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000156E1 File Offset: 0x000138E1
		public virtual void Visit(PrimaryExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000156F2 File Offset: 0x000138F2
		public virtual void ExplicitVisit(PrimaryExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00015718 File Offset: 0x00013918
		public virtual void Visit(Literal node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00015729 File Offset: 0x00013929
		public virtual void ExplicitVisit(Literal node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001575D File Offset: 0x0001395D
		public virtual void Visit(IntegerLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001576E File Offset: 0x0001396E
		public virtual void ExplicitVisit(IntegerLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000157A9 File Offset: 0x000139A9
		public virtual void Visit(NumericLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000157BA File Offset: 0x000139BA
		public virtual void ExplicitVisit(NumericLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000157F5 File Offset: 0x000139F5
		public virtual void Visit(RealLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00015806 File Offset: 0x00013A06
		public virtual void ExplicitVisit(RealLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00015841 File Offset: 0x00013A41
		public virtual void Visit(MoneyLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00015852 File Offset: 0x00013A52
		public virtual void ExplicitVisit(MoneyLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001588D File Offset: 0x00013A8D
		public virtual void Visit(BinaryLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001589E File Offset: 0x00013A9E
		public virtual void ExplicitVisit(BinaryLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000158D9 File Offset: 0x00013AD9
		public virtual void Visit(StringLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000158EA File Offset: 0x00013AEA
		public virtual void ExplicitVisit(StringLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00015925 File Offset: 0x00013B25
		public virtual void Visit(NullLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00015936 File Offset: 0x00013B36
		public virtual void ExplicitVisit(NullLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00015971 File Offset: 0x00013B71
		public virtual void Visit(IdentifierLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00015982 File Offset: 0x00013B82
		public virtual void ExplicitVisit(IdentifierLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000159BD File Offset: 0x00013BBD
		public virtual void Visit(DefaultLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000159CE File Offset: 0x00013BCE
		public virtual void ExplicitVisit(DefaultLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00015A09 File Offset: 0x00013C09
		public virtual void Visit(MaxLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00015A1A File Offset: 0x00013C1A
		public virtual void ExplicitVisit(MaxLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00015A55 File Offset: 0x00013C55
		public virtual void Visit(OdbcLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00015A66 File Offset: 0x00013C66
		public virtual void ExplicitVisit(OdbcLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00015AA1 File Offset: 0x00013CA1
		public virtual void Visit(LiteralRange node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00015AB2 File Offset: 0x00013CB2
		public virtual void ExplicitVisit(LiteralRange node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00015AD1 File Offset: 0x00013CD1
		public virtual void Visit(ValueExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00015AE2 File Offset: 0x00013CE2
		public virtual void ExplicitVisit(ValueExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00015B0F File Offset: 0x00013D0F
		public virtual void Visit(VariableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00015B20 File Offset: 0x00013D20
		public virtual void ExplicitVisit(VariableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00015B54 File Offset: 0x00013D54
		public virtual void Visit(GlobalVariableExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00015B65 File Offset: 0x00013D65
		public virtual void ExplicitVisit(GlobalVariableExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00015B99 File Offset: 0x00013D99
		public virtual void Visit(IdentifierOrValueExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00015BAA File Offset: 0x00013DAA
		public virtual void ExplicitVisit(IdentifierOrValueExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00015BC9 File Offset: 0x00013DC9
		public virtual void Visit(SchemaObjectNameOrValueExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00015BDA File Offset: 0x00013DDA
		public virtual void ExplicitVisit(SchemaObjectNameOrValueExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00015BF9 File Offset: 0x00013DF9
		public virtual void Visit(ParenthesisExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00015C0A File Offset: 0x00013E0A
		public virtual void ExplicitVisit(ParenthesisExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00015C37 File Offset: 0x00013E37
		public virtual void Visit(ColumnReferenceExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00015C48 File Offset: 0x00013E48
		public virtual void ExplicitVisit(ColumnReferenceExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00015C75 File Offset: 0x00013E75
		public virtual void Visit(NextValueForExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00015C86 File Offset: 0x00013E86
		public virtual void ExplicitVisit(NextValueForExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00015CB3 File Offset: 0x00013EB3
		public virtual void Visit(SequenceStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00015CC4 File Offset: 0x00013EC4
		public virtual void ExplicitVisit(SequenceStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00015CEA File Offset: 0x00013EEA
		public virtual void Visit(SequenceOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00015CFB File Offset: 0x00013EFB
		public virtual void ExplicitVisit(SequenceOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00015D1A File Offset: 0x00013F1A
		public virtual void Visit(DataTypeSequenceOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00015D2B File Offset: 0x00013F2B
		public virtual void ExplicitVisit(DataTypeSequenceOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00015D51 File Offset: 0x00013F51
		public virtual void Visit(ScalarExpressionSequenceOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00015D62 File Offset: 0x00013F62
		public virtual void ExplicitVisit(ScalarExpressionSequenceOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00015D88 File Offset: 0x00013F88
		public virtual void Visit(CreateSequenceStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00015D99 File Offset: 0x00013F99
		public virtual void ExplicitVisit(CreateSequenceStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00015DC6 File Offset: 0x00013FC6
		public virtual void Visit(AlterSequenceStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00015DD7 File Offset: 0x00013FD7
		public virtual void ExplicitVisit(AlterSequenceStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00015E04 File Offset: 0x00014004
		public virtual void Visit(DropSequenceStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00015E15 File Offset: 0x00014015
		public virtual void ExplicitVisit(DropSequenceStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00015E42 File Offset: 0x00014042
		public virtual void Visit(AssemblyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00015E53 File Offset: 0x00014053
		public virtual void ExplicitVisit(AssemblyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00015E79 File Offset: 0x00014079
		public virtual void Visit(CreateAssemblyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00015E8A File Offset: 0x0001408A
		public virtual void ExplicitVisit(CreateAssemblyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00015EB7 File Offset: 0x000140B7
		public virtual void Visit(AlterAssemblyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00015EC8 File Offset: 0x000140C8
		public virtual void ExplicitVisit(AlterAssemblyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00015EF5 File Offset: 0x000140F5
		public virtual void Visit(AssemblyOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00015F06 File Offset: 0x00014106
		public virtual void ExplicitVisit(AssemblyOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00015F25 File Offset: 0x00014125
		public virtual void Visit(OnOffAssemblyOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00015F36 File Offset: 0x00014136
		public virtual void ExplicitVisit(OnOffAssemblyOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00015F5C File Offset: 0x0001415C
		public virtual void Visit(PermissionSetAssemblyOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00015F6D File Offset: 0x0001416D
		public virtual void ExplicitVisit(PermissionSetAssemblyOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00015F93 File Offset: 0x00014193
		public virtual void Visit(AddFileSpec node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00015FA4 File Offset: 0x000141A4
		public virtual void ExplicitVisit(AddFileSpec node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00015FC3 File Offset: 0x000141C3
		public virtual void Visit(CreateXmlSchemaCollectionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00015FD4 File Offset: 0x000141D4
		public virtual void ExplicitVisit(CreateXmlSchemaCollectionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00015FFA File Offset: 0x000141FA
		public virtual void Visit(AlterXmlSchemaCollectionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001600B File Offset: 0x0001420B
		public virtual void ExplicitVisit(AlterXmlSchemaCollectionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00016031 File Offset: 0x00014231
		public virtual void Visit(DropXmlSchemaCollectionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00016042 File Offset: 0x00014242
		public virtual void ExplicitVisit(DropXmlSchemaCollectionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00016068 File Offset: 0x00014268
		public virtual void Visit(AssemblyName node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00016079 File Offset: 0x00014279
		public virtual void ExplicitVisit(AssemblyName node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00016098 File Offset: 0x00014298
		public virtual void Visit(AlterTableStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000160A9 File Offset: 0x000142A9
		public virtual void ExplicitVisit(AlterTableStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000160CF File Offset: 0x000142CF
		public virtual void Visit(AlterTableRebuildStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000160E0 File Offset: 0x000142E0
		public virtual void ExplicitVisit(AlterTableRebuildStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001610D File Offset: 0x0001430D
		public virtual void Visit(AlterTableChangeTrackingModificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001611E File Offset: 0x0001431E
		public virtual void ExplicitVisit(AlterTableChangeTrackingModificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001614B File Offset: 0x0001434B
		public virtual void Visit(AlterTableFileTableNamespaceStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001615C File Offset: 0x0001435C
		public virtual void ExplicitVisit(AlterTableFileTableNamespaceStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00016189 File Offset: 0x00014389
		public virtual void Visit(AlterTableSetStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001619A File Offset: 0x0001439A
		public virtual void ExplicitVisit(AlterTableSetStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000161C7 File Offset: 0x000143C7
		public virtual void Visit(TableOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000161D8 File Offset: 0x000143D8
		public virtual void ExplicitVisit(TableOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000161F7 File Offset: 0x000143F7
		public virtual void Visit(LockEscalationTableOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00016208 File Offset: 0x00014408
		public virtual void ExplicitVisit(LockEscalationTableOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001622E File Offset: 0x0001442E
		public virtual void Visit(FileStreamOnTableOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001623F File Offset: 0x0001443F
		public virtual void ExplicitVisit(FileStreamOnTableOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00016265 File Offset: 0x00014465
		public virtual void Visit(FileTableDirectoryTableOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00016276 File Offset: 0x00014476
		public virtual void ExplicitVisit(FileTableDirectoryTableOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001629C File Offset: 0x0001449C
		public virtual void Visit(FileTableCollateFileNameTableOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000162AD File Offset: 0x000144AD
		public virtual void ExplicitVisit(FileTableCollateFileNameTableOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000162D3 File Offset: 0x000144D3
		public virtual void Visit(FileTableConstraintNameTableOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000162E4 File Offset: 0x000144E4
		public virtual void ExplicitVisit(FileTableConstraintNameTableOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001630A File Offset: 0x0001450A
		public virtual void Visit(AlterTableAddTableElementStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001631B File Offset: 0x0001451B
		public virtual void ExplicitVisit(AlterTableAddTableElementStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00016348 File Offset: 0x00014548
		public virtual void Visit(AlterTableConstraintModificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00016359 File Offset: 0x00014559
		public virtual void ExplicitVisit(AlterTableConstraintModificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00016386 File Offset: 0x00014586
		public virtual void Visit(AlterTableSwitchStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00016397 File Offset: 0x00014597
		public virtual void ExplicitVisit(AlterTableSwitchStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000163C4 File Offset: 0x000145C4
		public virtual void Visit(DropClusteredConstraintOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000163D5 File Offset: 0x000145D5
		public virtual void ExplicitVisit(DropClusteredConstraintOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000163F4 File Offset: 0x000145F4
		public virtual void Visit(DropClusteredConstraintStateOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00016405 File Offset: 0x00014605
		public virtual void ExplicitVisit(DropClusteredConstraintStateOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001642B File Offset: 0x0001462B
		public virtual void Visit(DropClusteredConstraintValueOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001643C File Offset: 0x0001463C
		public virtual void ExplicitVisit(DropClusteredConstraintValueOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00016462 File Offset: 0x00014662
		public virtual void Visit(DropClusteredConstraintMoveOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00016473 File Offset: 0x00014673
		public virtual void ExplicitVisit(DropClusteredConstraintMoveOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00016499 File Offset: 0x00014699
		public virtual void Visit(AlterTableDropTableElement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000164AA File Offset: 0x000146AA
		public virtual void ExplicitVisit(AlterTableDropTableElement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000164C9 File Offset: 0x000146C9
		public virtual void Visit(AlterTableDropTableElementStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000164DA File Offset: 0x000146DA
		public virtual void ExplicitVisit(AlterTableDropTableElementStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00016507 File Offset: 0x00014707
		public virtual void Visit(AlterTableTriggerModificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00016518 File Offset: 0x00014718
		public virtual void ExplicitVisit(AlterTableTriggerModificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00016545 File Offset: 0x00014745
		public virtual void Visit(EnableDisableTriggerStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00016556 File Offset: 0x00014756
		public virtual void ExplicitVisit(EnableDisableTriggerStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001657C File Offset: 0x0001477C
		public virtual void Visit(TryCatchStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001658D File Offset: 0x0001478D
		public virtual void ExplicitVisit(TryCatchStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000165B3 File Offset: 0x000147B3
		public virtual void Visit(CreateTypeStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000165C4 File Offset: 0x000147C4
		public virtual void ExplicitVisit(CreateTypeStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000165EA File Offset: 0x000147EA
		public virtual void Visit(CreateTypeUdtStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000165FB File Offset: 0x000147FB
		public virtual void ExplicitVisit(CreateTypeUdtStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00016628 File Offset: 0x00014828
		public virtual void Visit(CreateTypeUddtStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00016639 File Offset: 0x00014839
		public virtual void ExplicitVisit(CreateTypeUddtStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00016666 File Offset: 0x00014866
		public virtual void Visit(CreateSynonymStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00016677 File Offset: 0x00014877
		public virtual void ExplicitVisit(CreateSynonymStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001669D File Offset: 0x0001489D
		public virtual void Visit(ExecuteAsClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000166AE File Offset: 0x000148AE
		public virtual void ExplicitVisit(ExecuteAsClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000166CD File Offset: 0x000148CD
		public virtual void Visit(QueueOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000166DE File Offset: 0x000148DE
		public virtual void ExplicitVisit(QueueOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000166FD File Offset: 0x000148FD
		public virtual void Visit(QueueStateOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001670E File Offset: 0x0001490E
		public virtual void ExplicitVisit(QueueStateOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00016734 File Offset: 0x00014934
		public virtual void Visit(QueueProcedureOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00016745 File Offset: 0x00014945
		public virtual void ExplicitVisit(QueueProcedureOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001676B File Offset: 0x0001496B
		public virtual void Visit(QueueValueOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001677C File Offset: 0x0001497C
		public virtual void ExplicitVisit(QueueValueOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000167A2 File Offset: 0x000149A2
		public virtual void Visit(QueueExecuteAsOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000167B3 File Offset: 0x000149B3
		public virtual void ExplicitVisit(QueueExecuteAsOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000167D9 File Offset: 0x000149D9
		public virtual void Visit(RouteOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000167EA File Offset: 0x000149EA
		public virtual void ExplicitVisit(RouteOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00016809 File Offset: 0x00014A09
		public virtual void Visit(RouteStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001681A File Offset: 0x00014A1A
		public virtual void ExplicitVisit(RouteStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00016840 File Offset: 0x00014A40
		public virtual void Visit(AlterRouteStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00016851 File Offset: 0x00014A51
		public virtual void ExplicitVisit(AlterRouteStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001687E File Offset: 0x00014A7E
		public virtual void Visit(CreateRouteStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001688F File Offset: 0x00014A8F
		public virtual void ExplicitVisit(CreateRouteStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000168BC File Offset: 0x00014ABC
		public virtual void Visit(QueueStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000168CD File Offset: 0x00014ACD
		public virtual void ExplicitVisit(QueueStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000168F3 File Offset: 0x00014AF3
		public virtual void Visit(AlterQueueStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00016904 File Offset: 0x00014B04
		public virtual void ExplicitVisit(AlterQueueStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00016931 File Offset: 0x00014B31
		public virtual void Visit(CreateQueueStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00016942 File Offset: 0x00014B42
		public virtual void ExplicitVisit(CreateQueueStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001696F File Offset: 0x00014B6F
		public virtual void Visit(IndexStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00016980 File Offset: 0x00014B80
		public virtual void ExplicitVisit(IndexStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000169A6 File Offset: 0x00014BA6
		public virtual void Visit(PartitionSpecifier node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000169B7 File Offset: 0x00014BB7
		public virtual void ExplicitVisit(PartitionSpecifier node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000169D6 File Offset: 0x00014BD6
		public virtual void Visit(AlterIndexStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000169E7 File Offset: 0x00014BE7
		public virtual void ExplicitVisit(AlterIndexStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00016A14 File Offset: 0x00014C14
		public virtual void Visit(CreateXmlIndexStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00016A25 File Offset: 0x00014C25
		public virtual void ExplicitVisit(CreateXmlIndexStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00016A52 File Offset: 0x00014C52
		public virtual void Visit(FileGroupOrPartitionScheme node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00016A63 File Offset: 0x00014C63
		public virtual void ExplicitVisit(FileGroupOrPartitionScheme node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00016A82 File Offset: 0x00014C82
		public virtual void Visit(CreateIndexStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00016A93 File Offset: 0x00014C93
		public virtual void ExplicitVisit(CreateIndexStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00016AC0 File Offset: 0x00014CC0
		public virtual void Visit(IndexOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00016AD1 File Offset: 0x00014CD1
		public virtual void ExplicitVisit(IndexOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00016AF0 File Offset: 0x00014CF0
		public virtual void Visit(IndexStateOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00016B01 File Offset: 0x00014D01
		public virtual void ExplicitVisit(IndexStateOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00016B27 File Offset: 0x00014D27
		public virtual void Visit(IndexExpressionOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00016B38 File Offset: 0x00014D38
		public virtual void ExplicitVisit(IndexExpressionOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00016B5E File Offset: 0x00014D5E
		public virtual void Visit(FullTextIndexColumn node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00016B6F File Offset: 0x00014D6F
		public virtual void ExplicitVisit(FullTextIndexColumn node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00016B8E File Offset: 0x00014D8E
		public virtual void Visit(CreateFullTextIndexStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00016B9F File Offset: 0x00014D9F
		public virtual void ExplicitVisit(CreateFullTextIndexStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00016BC5 File Offset: 0x00014DC5
		public virtual void Visit(FullTextIndexOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00016BD6 File Offset: 0x00014DD6
		public virtual void ExplicitVisit(FullTextIndexOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00016BF5 File Offset: 0x00014DF5
		public virtual void Visit(ChangeTrackingFullTextIndexOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00016C06 File Offset: 0x00014E06
		public virtual void ExplicitVisit(ChangeTrackingFullTextIndexOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00016C2C File Offset: 0x00014E2C
		public virtual void Visit(StopListFullTextIndexOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00016C3D File Offset: 0x00014E3D
		public virtual void ExplicitVisit(StopListFullTextIndexOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00016C63 File Offset: 0x00014E63
		public virtual void Visit(SearchPropertyListFullTextIndexOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00016C74 File Offset: 0x00014E74
		public virtual void ExplicitVisit(SearchPropertyListFullTextIndexOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00016C9A File Offset: 0x00014E9A
		public virtual void Visit(FullTextCatalogAndFileGroup node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00016CAB File Offset: 0x00014EAB
		public virtual void ExplicitVisit(FullTextCatalogAndFileGroup node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00016CCA File Offset: 0x00014ECA
		public virtual void Visit(EventTypeGroupContainer node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00016CDB File Offset: 0x00014EDB
		public virtual void ExplicitVisit(EventTypeGroupContainer node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00016CFA File Offset: 0x00014EFA
		public virtual void Visit(EventTypeContainer node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00016D0B File Offset: 0x00014F0B
		public virtual void ExplicitVisit(EventTypeContainer node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00016D31 File Offset: 0x00014F31
		public virtual void Visit(EventGroupContainer node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00016D42 File Offset: 0x00014F42
		public virtual void ExplicitVisit(EventGroupContainer node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00016D68 File Offset: 0x00014F68
		public virtual void Visit(CreateEventNotificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00016D79 File Offset: 0x00014F79
		public virtual void ExplicitVisit(CreateEventNotificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00016D9F File Offset: 0x00014F9F
		public virtual void Visit(EventNotificationObjectScope node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00016DB0 File Offset: 0x00014FB0
		public virtual void ExplicitVisit(EventNotificationObjectScope node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00016DCF File Offset: 0x00014FCF
		public virtual void Visit(MasterKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00016DE0 File Offset: 0x00014FE0
		public virtual void ExplicitVisit(MasterKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00016E06 File Offset: 0x00015006
		public virtual void Visit(CreateMasterKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00016E17 File Offset: 0x00015017
		public virtual void ExplicitVisit(CreateMasterKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00016E44 File Offset: 0x00015044
		public virtual void Visit(AlterMasterKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00016E55 File Offset: 0x00015055
		public virtual void ExplicitVisit(AlterMasterKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00016E82 File Offset: 0x00015082
		public virtual void Visit(ApplicationRoleOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00016E93 File Offset: 0x00015093
		public virtual void ExplicitVisit(ApplicationRoleOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00016EB2 File Offset: 0x000150B2
		public virtual void Visit(ApplicationRoleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00016EC3 File Offset: 0x000150C3
		public virtual void ExplicitVisit(ApplicationRoleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00016EE9 File Offset: 0x000150E9
		public virtual void Visit(CreateApplicationRoleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00016EFA File Offset: 0x000150FA
		public virtual void ExplicitVisit(CreateApplicationRoleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00016F27 File Offset: 0x00015127
		public virtual void Visit(AlterApplicationRoleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00016F38 File Offset: 0x00015138
		public virtual void ExplicitVisit(AlterApplicationRoleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00016F65 File Offset: 0x00015165
		public virtual void Visit(RoleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00016F76 File Offset: 0x00015176
		public virtual void ExplicitVisit(RoleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00016F9C File Offset: 0x0001519C
		public virtual void Visit(CreateRoleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00016FAD File Offset: 0x000151AD
		public virtual void ExplicitVisit(CreateRoleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00016FDA File Offset: 0x000151DA
		public virtual void Visit(AlterRoleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00016FEB File Offset: 0x000151EB
		public virtual void ExplicitVisit(AlterRoleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00017018 File Offset: 0x00015218
		public virtual void Visit(AlterRoleAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00017029 File Offset: 0x00015229
		public virtual void ExplicitVisit(AlterRoleAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00017048 File Offset: 0x00015248
		public virtual void Visit(RenameAlterRoleAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00017059 File Offset: 0x00015259
		public virtual void ExplicitVisit(RenameAlterRoleAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001707F File Offset: 0x0001527F
		public virtual void Visit(AddMemberAlterRoleAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00017090 File Offset: 0x00015290
		public virtual void ExplicitVisit(AddMemberAlterRoleAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000170B6 File Offset: 0x000152B6
		public virtual void Visit(DropMemberAlterRoleAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000170C7 File Offset: 0x000152C7
		public virtual void ExplicitVisit(DropMemberAlterRoleAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000170ED File Offset: 0x000152ED
		public virtual void Visit(CreateServerRoleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000170FE File Offset: 0x000152FE
		public virtual void ExplicitVisit(CreateServerRoleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00017132 File Offset: 0x00015332
		public virtual void Visit(AlterServerRoleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00017143 File Offset: 0x00015343
		public virtual void ExplicitVisit(AlterServerRoleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00017177 File Offset: 0x00015377
		public virtual void Visit(DropServerRoleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00017188 File Offset: 0x00015388
		public virtual void ExplicitVisit(DropServerRoleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000171B5 File Offset: 0x000153B5
		public virtual void Visit(UserLoginOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000171C6 File Offset: 0x000153C6
		public virtual void ExplicitVisit(UserLoginOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000171E5 File Offset: 0x000153E5
		public virtual void Visit(UserStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000171F6 File Offset: 0x000153F6
		public virtual void ExplicitVisit(UserStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001721C File Offset: 0x0001541C
		public virtual void Visit(CreateUserStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001722D File Offset: 0x0001542D
		public virtual void ExplicitVisit(CreateUserStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001725A File Offset: 0x0001545A
		public virtual void Visit(AlterUserStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001726B File Offset: 0x0001546B
		public virtual void ExplicitVisit(AlterUserStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00017298 File Offset: 0x00015498
		public virtual void Visit(StatisticsOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000172A9 File Offset: 0x000154A9
		public virtual void ExplicitVisit(StatisticsOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000172C8 File Offset: 0x000154C8
		public virtual void Visit(LiteralStatisticsOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000172D9 File Offset: 0x000154D9
		public virtual void ExplicitVisit(LiteralStatisticsOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000172FF File Offset: 0x000154FF
		public virtual void Visit(CreateStatisticsStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00017310 File Offset: 0x00015510
		public virtual void ExplicitVisit(CreateStatisticsStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00017336 File Offset: 0x00015536
		public virtual void Visit(UpdateStatisticsStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00017347 File Offset: 0x00015547
		public virtual void ExplicitVisit(UpdateStatisticsStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001736D File Offset: 0x0001556D
		public virtual void Visit(ReturnStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001737E File Offset: 0x0001557E
		public virtual void ExplicitVisit(ReturnStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000173A4 File Offset: 0x000155A4
		public virtual void Visit(DeclareCursorStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000173B5 File Offset: 0x000155B5
		public virtual void ExplicitVisit(DeclareCursorStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000173DB File Offset: 0x000155DB
		public virtual void Visit(CursorDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000173EC File Offset: 0x000155EC
		public virtual void ExplicitVisit(CursorDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001740B File Offset: 0x0001560B
		public virtual void Visit(CursorOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001741C File Offset: 0x0001561C
		public virtual void ExplicitVisit(CursorOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001743B File Offset: 0x0001563B
		public virtual void Visit(SetVariableStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001744C File Offset: 0x0001564C
		public virtual void ExplicitVisit(SetVariableStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00017472 File Offset: 0x00015672
		public virtual void Visit(CursorId node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00017483 File Offset: 0x00015683
		public virtual void ExplicitVisit(CursorId node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000174A2 File Offset: 0x000156A2
		public virtual void Visit(CursorStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000174B3 File Offset: 0x000156B3
		public virtual void ExplicitVisit(CursorStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000174D9 File Offset: 0x000156D9
		public virtual void Visit(OpenCursorStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000174EA File Offset: 0x000156EA
		public virtual void ExplicitVisit(OpenCursorStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00017517 File Offset: 0x00015717
		public virtual void Visit(CloseCursorStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00017528 File Offset: 0x00015728
		public virtual void ExplicitVisit(CloseCursorStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00017555 File Offset: 0x00015755
		public virtual void Visit(CryptoMechanism node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00017566 File Offset: 0x00015766
		public virtual void ExplicitVisit(CryptoMechanism node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00017585 File Offset: 0x00015785
		public virtual void Visit(OpenSymmetricKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00017596 File Offset: 0x00015796
		public virtual void ExplicitVisit(OpenSymmetricKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000175BC File Offset: 0x000157BC
		public virtual void Visit(CloseSymmetricKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000175CD File Offset: 0x000157CD
		public virtual void ExplicitVisit(CloseSymmetricKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000175F3 File Offset: 0x000157F3
		public virtual void Visit(OpenMasterKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00017604 File Offset: 0x00015804
		public virtual void ExplicitVisit(OpenMasterKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001762A File Offset: 0x0001582A
		public virtual void Visit(CloseMasterKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001763B File Offset: 0x0001583B
		public virtual void ExplicitVisit(CloseMasterKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00017661 File Offset: 0x00015861
		public virtual void Visit(DeallocateCursorStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00017672 File Offset: 0x00015872
		public virtual void ExplicitVisit(DeallocateCursorStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001769F File Offset: 0x0001589F
		public virtual void Visit(FetchType node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000176B0 File Offset: 0x000158B0
		public virtual void ExplicitVisit(FetchType node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000176CF File Offset: 0x000158CF
		public virtual void Visit(FetchCursorStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000176E0 File Offset: 0x000158E0
		public virtual void ExplicitVisit(FetchCursorStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001770D File Offset: 0x0001590D
		public virtual void Visit(WhereClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001771E File Offset: 0x0001591E
		public virtual void ExplicitVisit(WhereClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001773D File Offset: 0x0001593D
		public virtual void Visit(DropUnownedObjectStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001774E File Offset: 0x0001594E
		public virtual void ExplicitVisit(DropUnownedObjectStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00017774 File Offset: 0x00015974
		public virtual void Visit(DropObjectsStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00017785 File Offset: 0x00015985
		public virtual void ExplicitVisit(DropObjectsStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000177AB File Offset: 0x000159AB
		public virtual void Visit(DropDatabaseStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000177BC File Offset: 0x000159BC
		public virtual void ExplicitVisit(DropDatabaseStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000177E2 File Offset: 0x000159E2
		public virtual void Visit(DropChildObjectsStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000177F3 File Offset: 0x000159F3
		public virtual void ExplicitVisit(DropChildObjectsStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00017819 File Offset: 0x00015A19
		public virtual void Visit(DropIndexStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001782A File Offset: 0x00015A2A
		public virtual void ExplicitVisit(DropIndexStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00017850 File Offset: 0x00015A50
		public virtual void Visit(DropIndexClauseBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00017861 File Offset: 0x00015A61
		public virtual void ExplicitVisit(DropIndexClauseBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00017880 File Offset: 0x00015A80
		public virtual void Visit(BackwardsCompatibleDropIndexClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00017891 File Offset: 0x00015A91
		public virtual void ExplicitVisit(BackwardsCompatibleDropIndexClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000178B7 File Offset: 0x00015AB7
		public virtual void Visit(DropIndexClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000178C8 File Offset: 0x00015AC8
		public virtual void ExplicitVisit(DropIndexClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000178EE File Offset: 0x00015AEE
		public virtual void Visit(MoveToDropIndexOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000178FF File Offset: 0x00015AFF
		public virtual void ExplicitVisit(MoveToDropIndexOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00017925 File Offset: 0x00015B25
		public virtual void Visit(FileStreamOnDropIndexOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00017936 File Offset: 0x00015B36
		public virtual void ExplicitVisit(FileStreamOnDropIndexOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001795C File Offset: 0x00015B5C
		public virtual void Visit(DropStatisticsStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001796D File Offset: 0x00015B6D
		public virtual void ExplicitVisit(DropStatisticsStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001799A File Offset: 0x00015B9A
		public virtual void Visit(DropTableStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000179AB File Offset: 0x00015BAB
		public virtual void ExplicitVisit(DropTableStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000179D8 File Offset: 0x00015BD8
		public virtual void Visit(DropProcedureStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000179E9 File Offset: 0x00015BE9
		public virtual void ExplicitVisit(DropProcedureStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00017A16 File Offset: 0x00015C16
		public virtual void Visit(DropFunctionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00017A27 File Offset: 0x00015C27
		public virtual void ExplicitVisit(DropFunctionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00017A54 File Offset: 0x00015C54
		public virtual void Visit(DropViewStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00017A65 File Offset: 0x00015C65
		public virtual void ExplicitVisit(DropViewStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00017A92 File Offset: 0x00015C92
		public virtual void Visit(DropDefaultStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00017AA3 File Offset: 0x00015CA3
		public virtual void ExplicitVisit(DropDefaultStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00017AD0 File Offset: 0x00015CD0
		public virtual void Visit(DropRuleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00017AE1 File Offset: 0x00015CE1
		public virtual void ExplicitVisit(DropRuleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00017B0E File Offset: 0x00015D0E
		public virtual void Visit(DropTriggerStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00017B1F File Offset: 0x00015D1F
		public virtual void ExplicitVisit(DropTriggerStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00017B4C File Offset: 0x00015D4C
		public virtual void Visit(DropSchemaStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00017B5D File Offset: 0x00015D5D
		public virtual void ExplicitVisit(DropSchemaStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00017B83 File Offset: 0x00015D83
		public virtual void Visit(RaiseErrorLegacyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00017B94 File Offset: 0x00015D94
		public virtual void ExplicitVisit(RaiseErrorLegacyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00017BBA File Offset: 0x00015DBA
		public virtual void Visit(RaiseErrorStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00017BCB File Offset: 0x00015DCB
		public virtual void ExplicitVisit(RaiseErrorStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00017BF1 File Offset: 0x00015DF1
		public virtual void Visit(ThrowStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00017C02 File Offset: 0x00015E02
		public virtual void ExplicitVisit(ThrowStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00017C28 File Offset: 0x00015E28
		public virtual void Visit(UseStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00017C39 File Offset: 0x00015E39
		public virtual void ExplicitVisit(UseStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00017C5F File Offset: 0x00015E5F
		public virtual void Visit(KillStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00017C70 File Offset: 0x00015E70
		public virtual void ExplicitVisit(KillStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00017C96 File Offset: 0x00015E96
		public virtual void Visit(KillQueryNotificationSubscriptionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00017CA7 File Offset: 0x00015EA7
		public virtual void ExplicitVisit(KillQueryNotificationSubscriptionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00017CCD File Offset: 0x00015ECD
		public virtual void Visit(KillStatsJobStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00017CDE File Offset: 0x00015EDE
		public virtual void ExplicitVisit(KillStatsJobStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00017D04 File Offset: 0x00015F04
		public virtual void Visit(CheckpointStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00017D15 File Offset: 0x00015F15
		public virtual void ExplicitVisit(CheckpointStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00017D3B File Offset: 0x00015F3B
		public virtual void Visit(ReconfigureStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00017D4C File Offset: 0x00015F4C
		public virtual void ExplicitVisit(ReconfigureStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00017D72 File Offset: 0x00015F72
		public virtual void Visit(ShutdownStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00017D83 File Offset: 0x00015F83
		public virtual void ExplicitVisit(ShutdownStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00017DA9 File Offset: 0x00015FA9
		public virtual void Visit(SetUserStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00017DBA File Offset: 0x00015FBA
		public virtual void ExplicitVisit(SetUserStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00017DE0 File Offset: 0x00015FE0
		public virtual void Visit(TruncateTableStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00017DF1 File Offset: 0x00015FF1
		public virtual void ExplicitVisit(TruncateTableStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00017E17 File Offset: 0x00016017
		public virtual void Visit(SetOnOffStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00017E28 File Offset: 0x00016028
		public virtual void ExplicitVisit(SetOnOffStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00017E4E File Offset: 0x0001604E
		public virtual void Visit(PredicateSetStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00017E5F File Offset: 0x0001605F
		public virtual void ExplicitVisit(PredicateSetStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00017E8C File Offset: 0x0001608C
		public virtual void Visit(SetStatisticsStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00017E9D File Offset: 0x0001609D
		public virtual void ExplicitVisit(SetStatisticsStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00017ECA File Offset: 0x000160CA
		public virtual void Visit(SetRowCountStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00017EDB File Offset: 0x000160DB
		public virtual void ExplicitVisit(SetRowCountStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00017F01 File Offset: 0x00016101
		public virtual void Visit(SetOffsetsStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00017F12 File Offset: 0x00016112
		public virtual void ExplicitVisit(SetOffsetsStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00017F3F File Offset: 0x0001613F
		public virtual void Visit(SetCommand node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00017F50 File Offset: 0x00016150
		public virtual void ExplicitVisit(SetCommand node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00017F6F File Offset: 0x0001616F
		public virtual void Visit(GeneralSetCommand node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00017F80 File Offset: 0x00016180
		public virtual void ExplicitVisit(GeneralSetCommand node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00017FA6 File Offset: 0x000161A6
		public virtual void Visit(SetFipsFlaggerCommand node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00017FB7 File Offset: 0x000161B7
		public virtual void ExplicitVisit(SetFipsFlaggerCommand node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00017FDD File Offset: 0x000161DD
		public virtual void Visit(SetCommandStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00017FEE File Offset: 0x000161EE
		public virtual void ExplicitVisit(SetCommandStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00018014 File Offset: 0x00016214
		public virtual void Visit(SetTransactionIsolationLevelStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00018025 File Offset: 0x00016225
		public virtual void ExplicitVisit(SetTransactionIsolationLevelStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001804B File Offset: 0x0001624B
		public virtual void Visit(SetTextSizeStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001805C File Offset: 0x0001625C
		public virtual void ExplicitVisit(SetTextSizeStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00018082 File Offset: 0x00016282
		public virtual void Visit(SetIdentityInsertStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00018093 File Offset: 0x00016293
		public virtual void ExplicitVisit(SetIdentityInsertStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000180C0 File Offset: 0x000162C0
		public virtual void Visit(SetErrorLevelStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000180D1 File Offset: 0x000162D1
		public virtual void ExplicitVisit(SetErrorLevelStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000180F7 File Offset: 0x000162F7
		public virtual void Visit(CreateDatabaseStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00018108 File Offset: 0x00016308
		public virtual void ExplicitVisit(CreateDatabaseStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001812E File Offset: 0x0001632E
		public virtual void Visit(FileDeclaration node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001813F File Offset: 0x0001633F
		public virtual void ExplicitVisit(FileDeclaration node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001815E File Offset: 0x0001635E
		public virtual void Visit(FileDeclarationOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001816F File Offset: 0x0001636F
		public virtual void ExplicitVisit(FileDeclarationOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001818E File Offset: 0x0001638E
		public virtual void Visit(NameFileDeclarationOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001819F File Offset: 0x0001639F
		public virtual void ExplicitVisit(NameFileDeclarationOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000181C5 File Offset: 0x000163C5
		public virtual void Visit(FileNameFileDeclarationOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000181D6 File Offset: 0x000163D6
		public virtual void ExplicitVisit(FileNameFileDeclarationOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000181FC File Offset: 0x000163FC
		public virtual void Visit(SizeFileDeclarationOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001820D File Offset: 0x0001640D
		public virtual void ExplicitVisit(SizeFileDeclarationOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00018233 File Offset: 0x00016433
		public virtual void Visit(MaxSizeFileDeclarationOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00018244 File Offset: 0x00016444
		public virtual void ExplicitVisit(MaxSizeFileDeclarationOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001826A File Offset: 0x0001646A
		public virtual void Visit(FileGrowthFileDeclarationOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001827B File Offset: 0x0001647B
		public virtual void ExplicitVisit(FileGrowthFileDeclarationOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000182A1 File Offset: 0x000164A1
		public virtual void Visit(FileGroupDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000182B2 File Offset: 0x000164B2
		public virtual void ExplicitVisit(FileGroupDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000182D1 File Offset: 0x000164D1
		public virtual void Visit(AlterDatabaseStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000182E2 File Offset: 0x000164E2
		public virtual void ExplicitVisit(AlterDatabaseStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00018308 File Offset: 0x00016508
		public virtual void Visit(AlterDatabaseCollateStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00018319 File Offset: 0x00016519
		public virtual void ExplicitVisit(AlterDatabaseCollateStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00018346 File Offset: 0x00016546
		public virtual void Visit(AlterDatabaseRebuildLogStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00018357 File Offset: 0x00016557
		public virtual void ExplicitVisit(AlterDatabaseRebuildLogStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00018384 File Offset: 0x00016584
		public virtual void Visit(AlterDatabaseAddFileStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00018395 File Offset: 0x00016595
		public virtual void ExplicitVisit(AlterDatabaseAddFileStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000183C2 File Offset: 0x000165C2
		public virtual void Visit(AlterDatabaseAddFileGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000183D3 File Offset: 0x000165D3
		public virtual void ExplicitVisit(AlterDatabaseAddFileGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00018400 File Offset: 0x00016600
		public virtual void Visit(AlterDatabaseRemoveFileGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00018411 File Offset: 0x00016611
		public virtual void ExplicitVisit(AlterDatabaseRemoveFileGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001843E File Offset: 0x0001663E
		public virtual void Visit(AlterDatabaseRemoveFileStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001844F File Offset: 0x0001664F
		public virtual void ExplicitVisit(AlterDatabaseRemoveFileStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001847C File Offset: 0x0001667C
		public virtual void Visit(AlterDatabaseModifyNameStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001848D File Offset: 0x0001668D
		public virtual void ExplicitVisit(AlterDatabaseModifyNameStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000184BA File Offset: 0x000166BA
		public virtual void Visit(AlterDatabaseModifyFileStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000184CB File Offset: 0x000166CB
		public virtual void ExplicitVisit(AlterDatabaseModifyFileStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000184F8 File Offset: 0x000166F8
		public virtual void Visit(AlterDatabaseModifyFileGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00018509 File Offset: 0x00016709
		public virtual void ExplicitVisit(AlterDatabaseModifyFileGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00018536 File Offset: 0x00016736
		public virtual void Visit(AlterDatabaseTermination node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00018547 File Offset: 0x00016747
		public virtual void ExplicitVisit(AlterDatabaseTermination node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00018566 File Offset: 0x00016766
		public virtual void Visit(AlterDatabaseSetStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00018577 File Offset: 0x00016777
		public virtual void ExplicitVisit(AlterDatabaseSetStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000185A4 File Offset: 0x000167A4
		public virtual void Visit(DatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000185B5 File Offset: 0x000167B5
		public virtual void ExplicitVisit(DatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000185D4 File Offset: 0x000167D4
		public virtual void Visit(OnOffDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000185E5 File Offset: 0x000167E5
		public virtual void ExplicitVisit(OnOffDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001860B File Offset: 0x0001680B
		public virtual void Visit(ContainmentDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001861C File Offset: 0x0001681C
		public virtual void ExplicitVisit(ContainmentDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00018642 File Offset: 0x00016842
		public virtual void Visit(HadrDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00018653 File Offset: 0x00016853
		public virtual void ExplicitVisit(HadrDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00018679 File Offset: 0x00016879
		public virtual void Visit(HadrAvailabilityGroupDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001868A File Offset: 0x0001688A
		public virtual void ExplicitVisit(HadrAvailabilityGroupDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000186B7 File Offset: 0x000168B7
		public virtual void Visit(CursorDefaultDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000186C8 File Offset: 0x000168C8
		public virtual void ExplicitVisit(CursorDefaultDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000186EE File Offset: 0x000168EE
		public virtual void Visit(RecoveryDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000186FF File Offset: 0x000168FF
		public virtual void ExplicitVisit(RecoveryDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00018725 File Offset: 0x00016925
		public virtual void Visit(TargetRecoveryTimeDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00018736 File Offset: 0x00016936
		public virtual void ExplicitVisit(TargetRecoveryTimeDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001875C File Offset: 0x0001695C
		public virtual void Visit(PageVerifyDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001876D File Offset: 0x0001696D
		public virtual void ExplicitVisit(PageVerifyDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00018793 File Offset: 0x00016993
		public virtual void Visit(PartnerDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000187A4 File Offset: 0x000169A4
		public virtual void ExplicitVisit(PartnerDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000187CA File Offset: 0x000169CA
		public virtual void Visit(WitnessDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000187DB File Offset: 0x000169DB
		public virtual void ExplicitVisit(WitnessDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00018801 File Offset: 0x00016A01
		public virtual void Visit(ParameterizationDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00018812 File Offset: 0x00016A12
		public virtual void ExplicitVisit(ParameterizationDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00018838 File Offset: 0x00016A38
		public virtual void Visit(LiteralDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00018849 File Offset: 0x00016A49
		public virtual void ExplicitVisit(LiteralDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001886F File Offset: 0x00016A6F
		public virtual void Visit(IdentifierDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00018880 File Offset: 0x00016A80
		public virtual void ExplicitVisit(IdentifierDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000188A6 File Offset: 0x00016AA6
		public virtual void Visit(ChangeTrackingDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000188B7 File Offset: 0x00016AB7
		public virtual void ExplicitVisit(ChangeTrackingDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000188DD File Offset: 0x00016ADD
		public virtual void Visit(ChangeTrackingOptionDetail node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000188EE File Offset: 0x00016AEE
		public virtual void ExplicitVisit(ChangeTrackingOptionDetail node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001890D File Offset: 0x00016B0D
		public virtual void Visit(AutoCleanupChangeTrackingOptionDetail node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001891E File Offset: 0x00016B1E
		public virtual void ExplicitVisit(AutoCleanupChangeTrackingOptionDetail node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00018944 File Offset: 0x00016B44
		public virtual void Visit(ChangeRetentionChangeTrackingOptionDetail node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00018955 File Offset: 0x00016B55
		public virtual void ExplicitVisit(ChangeRetentionChangeTrackingOptionDetail node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001897B File Offset: 0x00016B7B
		public virtual void Visit(FileStreamDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001898C File Offset: 0x00016B8C
		public virtual void ExplicitVisit(FileStreamDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000189B2 File Offset: 0x00016BB2
		public virtual void Visit(MaxSizeDatabaseOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000189C3 File Offset: 0x00016BC3
		public virtual void ExplicitVisit(MaxSizeDatabaseOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x000189E9 File Offset: 0x00016BE9
		public virtual void Visit(AlterTableAlterColumnStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000189FA File Offset: 0x00016BFA
		public virtual void ExplicitVisit(AlterTableAlterColumnStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00018A27 File Offset: 0x00016C27
		public virtual void Visit(ColumnDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00018A38 File Offset: 0x00016C38
		public virtual void ExplicitVisit(ColumnDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00018A5E File Offset: 0x00016C5E
		public virtual void Visit(IdentityOptions node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00018A6F File Offset: 0x00016C6F
		public virtual void ExplicitVisit(IdentityOptions node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00018A8E File Offset: 0x00016C8E
		public virtual void Visit(ColumnStorageOptions node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00018A9F File Offset: 0x00016C9F
		public virtual void ExplicitVisit(ColumnStorageOptions node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00018ABE File Offset: 0x00016CBE
		public virtual void Visit(ConstraintDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00018ACF File Offset: 0x00016CCF
		public virtual void ExplicitVisit(ConstraintDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00018AEE File Offset: 0x00016CEE
		public virtual void Visit(CreateTableStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00018AFF File Offset: 0x00016CFF
		public virtual void ExplicitVisit(CreateTableStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00018B25 File Offset: 0x00016D25
		public virtual void Visit(FederationScheme node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00018B36 File Offset: 0x00016D36
		public virtual void ExplicitVisit(FederationScheme node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00018B55 File Offset: 0x00016D55
		public virtual void Visit(TableDataCompressionOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00018B66 File Offset: 0x00016D66
		public virtual void ExplicitVisit(TableDataCompressionOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00018B8C File Offset: 0x00016D8C
		public virtual void Visit(DataCompressionOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00018B9D File Offset: 0x00016D9D
		public virtual void ExplicitVisit(DataCompressionOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00018BC3 File Offset: 0x00016DC3
		public virtual void Visit(CompressionPartitionRange node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00018BD4 File Offset: 0x00016DD4
		public virtual void ExplicitVisit(CompressionPartitionRange node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00018BF3 File Offset: 0x00016DF3
		public virtual void Visit(CheckConstraintDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00018C04 File Offset: 0x00016E04
		public virtual void ExplicitVisit(CheckConstraintDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00018C2A File Offset: 0x00016E2A
		public virtual void Visit(DefaultConstraintDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00018C3B File Offset: 0x00016E3B
		public virtual void ExplicitVisit(DefaultConstraintDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00018C61 File Offset: 0x00016E61
		public virtual void Visit(ForeignKeyConstraintDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00018C72 File Offset: 0x00016E72
		public virtual void ExplicitVisit(ForeignKeyConstraintDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00018C98 File Offset: 0x00016E98
		public virtual void Visit(NullableConstraintDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00018CA9 File Offset: 0x00016EA9
		public virtual void ExplicitVisit(NullableConstraintDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00018CCF File Offset: 0x00016ECF
		public virtual void Visit(UniqueConstraintDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00018CE0 File Offset: 0x00016EE0
		public virtual void ExplicitVisit(UniqueConstraintDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00018D06 File Offset: 0x00016F06
		public virtual void Visit(BackupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00018D17 File Offset: 0x00016F17
		public virtual void ExplicitVisit(BackupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00018D3D File Offset: 0x00016F3D
		public virtual void Visit(BackupDatabaseStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00018D4E File Offset: 0x00016F4E
		public virtual void ExplicitVisit(BackupDatabaseStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00018D7B File Offset: 0x00016F7B
		public virtual void Visit(BackupTransactionLogStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00018D8C File Offset: 0x00016F8C
		public virtual void ExplicitVisit(BackupTransactionLogStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00018DB9 File Offset: 0x00016FB9
		public virtual void Visit(RestoreStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00018DCA File Offset: 0x00016FCA
		public virtual void ExplicitVisit(RestoreStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00018DF0 File Offset: 0x00016FF0
		public virtual void Visit(RestoreOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00018E01 File Offset: 0x00017001
		public virtual void ExplicitVisit(RestoreOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00018E20 File Offset: 0x00017020
		public virtual void Visit(ScalarExpressionRestoreOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00018E31 File Offset: 0x00017031
		public virtual void ExplicitVisit(ScalarExpressionRestoreOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00018E57 File Offset: 0x00017057
		public virtual void Visit(MoveRestoreOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00018E68 File Offset: 0x00017068
		public virtual void ExplicitVisit(MoveRestoreOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00018E8E File Offset: 0x0001708E
		public virtual void Visit(StopRestoreOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00018E9F File Offset: 0x0001709F
		public virtual void ExplicitVisit(StopRestoreOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00018EC5 File Offset: 0x000170C5
		public virtual void Visit(FileStreamRestoreOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00018ED6 File Offset: 0x000170D6
		public virtual void ExplicitVisit(FileStreamRestoreOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00018EFC File Offset: 0x000170FC
		public virtual void Visit(BackupOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00018F0D File Offset: 0x0001710D
		public virtual void ExplicitVisit(BackupOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00018F2C File Offset: 0x0001712C
		public virtual void Visit(DeviceInfo node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00018F3D File Offset: 0x0001713D
		public virtual void ExplicitVisit(DeviceInfo node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00018F5C File Offset: 0x0001715C
		public virtual void Visit(MirrorToClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00018F6D File Offset: 0x0001716D
		public virtual void ExplicitVisit(MirrorToClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00018F8C File Offset: 0x0001718C
		public virtual void Visit(BackupRestoreFileInfo node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00018F9D File Offset: 0x0001719D
		public virtual void ExplicitVisit(BackupRestoreFileInfo node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00018FBC File Offset: 0x000171BC
		public virtual void Visit(BulkInsertBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00018FCD File Offset: 0x000171CD
		public virtual void ExplicitVisit(BulkInsertBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00018FF3 File Offset: 0x000171F3
		public virtual void Visit(BulkInsertStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00019004 File Offset: 0x00017204
		public virtual void ExplicitVisit(BulkInsertStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00019031 File Offset: 0x00017231
		public virtual void Visit(InsertBulkStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00019042 File Offset: 0x00017242
		public virtual void ExplicitVisit(InsertBulkStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001906F File Offset: 0x0001726F
		public virtual void Visit(BulkInsertOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00019080 File Offset: 0x00017280
		public virtual void ExplicitVisit(BulkInsertOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001909F File Offset: 0x0001729F
		public virtual void Visit(LiteralBulkInsertOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000190B0 File Offset: 0x000172B0
		public virtual void ExplicitVisit(LiteralBulkInsertOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000190D6 File Offset: 0x000172D6
		public virtual void Visit(OrderBulkInsertOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000190E7 File Offset: 0x000172E7
		public virtual void ExplicitVisit(OrderBulkInsertOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001910D File Offset: 0x0001730D
		public virtual void Visit(ColumnDefinitionBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001911E File Offset: 0x0001731E
		public virtual void ExplicitVisit(ColumnDefinitionBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001913D File Offset: 0x0001733D
		public virtual void Visit(InsertBulkColumnDefinition node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001914E File Offset: 0x0001734E
		public virtual void ExplicitVisit(InsertBulkColumnDefinition node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001916D File Offset: 0x0001736D
		public virtual void Visit(DbccStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001917E File Offset: 0x0001737E
		public virtual void ExplicitVisit(DbccStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x000191A4 File Offset: 0x000173A4
		public virtual void Visit(DbccOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000191B5 File Offset: 0x000173B5
		public virtual void ExplicitVisit(DbccOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000191D4 File Offset: 0x000173D4
		public virtual void Visit(DbccNamedLiteral node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000191E5 File Offset: 0x000173E5
		public virtual void ExplicitVisit(DbccNamedLiteral node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00019204 File Offset: 0x00017404
		public virtual void Visit(CreateAsymmetricKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00019215 File Offset: 0x00017415
		public virtual void ExplicitVisit(CreateAsymmetricKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001923B File Offset: 0x0001743B
		public virtual void Visit(CreatePartitionFunctionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001924C File Offset: 0x0001744C
		public virtual void ExplicitVisit(CreatePartitionFunctionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00019272 File Offset: 0x00017472
		public virtual void Visit(PartitionParameterType node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00019283 File Offset: 0x00017483
		public virtual void ExplicitVisit(PartitionParameterType node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000192A2 File Offset: 0x000174A2
		public virtual void Visit(CreatePartitionSchemeStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000192B3 File Offset: 0x000174B3
		public virtual void ExplicitVisit(CreatePartitionSchemeStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x000192D9 File Offset: 0x000174D9
		public virtual void Visit(RemoteServiceBindingStatementBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000192EA File Offset: 0x000174EA
		public virtual void ExplicitVisit(RemoteServiceBindingStatementBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00019310 File Offset: 0x00017510
		public virtual void Visit(RemoteServiceBindingOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00019321 File Offset: 0x00017521
		public virtual void ExplicitVisit(RemoteServiceBindingOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00019340 File Offset: 0x00017540
		public virtual void Visit(OnOffRemoteServiceBindingOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00019351 File Offset: 0x00017551
		public virtual void ExplicitVisit(OnOffRemoteServiceBindingOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00019377 File Offset: 0x00017577
		public virtual void Visit(UserRemoteServiceBindingOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00019388 File Offset: 0x00017588
		public virtual void ExplicitVisit(UserRemoteServiceBindingOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000193AE File Offset: 0x000175AE
		public virtual void Visit(CreateRemoteServiceBindingStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000193BF File Offset: 0x000175BF
		public virtual void ExplicitVisit(CreateRemoteServiceBindingStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000193EC File Offset: 0x000175EC
		public virtual void Visit(AlterRemoteServiceBindingStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000193FD File Offset: 0x000175FD
		public virtual void ExplicitVisit(AlterRemoteServiceBindingStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001942A File Offset: 0x0001762A
		public virtual void Visit(EncryptionSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001943B File Offset: 0x0001763B
		public virtual void ExplicitVisit(EncryptionSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001945A File Offset: 0x0001765A
		public virtual void Visit(AssemblyEncryptionSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001946B File Offset: 0x0001766B
		public virtual void ExplicitVisit(AssemblyEncryptionSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00019491 File Offset: 0x00017691
		public virtual void Visit(FileEncryptionSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000194A2 File Offset: 0x000176A2
		public virtual void ExplicitVisit(FileEncryptionSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000194C8 File Offset: 0x000176C8
		public virtual void Visit(ProviderEncryptionSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000194D9 File Offset: 0x000176D9
		public virtual void ExplicitVisit(ProviderEncryptionSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000194FF File Offset: 0x000176FF
		public virtual void Visit(CertificateStatementBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00019510 File Offset: 0x00017710
		public virtual void ExplicitVisit(CertificateStatementBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00019536 File Offset: 0x00017736
		public virtual void Visit(AlterCertificateStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00019547 File Offset: 0x00017747
		public virtual void ExplicitVisit(AlterCertificateStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00019574 File Offset: 0x00017774
		public virtual void Visit(CreateCertificateStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00019585 File Offset: 0x00017785
		public virtual void ExplicitVisit(CreateCertificateStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000195B2 File Offset: 0x000177B2
		public virtual void Visit(CertificateOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000195C3 File Offset: 0x000177C3
		public virtual void ExplicitVisit(CertificateOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000195E2 File Offset: 0x000177E2
		public virtual void Visit(CreateContractStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000195F3 File Offset: 0x000177F3
		public virtual void ExplicitVisit(CreateContractStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00019619 File Offset: 0x00017819
		public virtual void Visit(ContractMessage node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001962A File Offset: 0x0001782A
		public virtual void ExplicitVisit(ContractMessage node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00019649 File Offset: 0x00017849
		public virtual void Visit(CredentialStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001965A File Offset: 0x0001785A
		public virtual void ExplicitVisit(CredentialStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00019680 File Offset: 0x00017880
		public virtual void Visit(CreateCredentialStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00019691 File Offset: 0x00017891
		public virtual void ExplicitVisit(CreateCredentialStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000196BE File Offset: 0x000178BE
		public virtual void Visit(AlterCredentialStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000196CF File Offset: 0x000178CF
		public virtual void ExplicitVisit(AlterCredentialStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000196FC File Offset: 0x000178FC
		public virtual void Visit(MessageTypeStatementBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001970D File Offset: 0x0001790D
		public virtual void ExplicitVisit(MessageTypeStatementBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00019733 File Offset: 0x00017933
		public virtual void Visit(CreateMessageTypeStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00019744 File Offset: 0x00017944
		public virtual void ExplicitVisit(CreateMessageTypeStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00019771 File Offset: 0x00017971
		public virtual void Visit(AlterMessageTypeStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00019782 File Offset: 0x00017982
		public virtual void ExplicitVisit(AlterMessageTypeStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000197AF File Offset: 0x000179AF
		public virtual void Visit(CreateAggregateStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000197C0 File Offset: 0x000179C0
		public virtual void ExplicitVisit(CreateAggregateStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000197E6 File Offset: 0x000179E6
		public virtual void Visit(CreateEndpointStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000197F7 File Offset: 0x000179F7
		public virtual void ExplicitVisit(CreateEndpointStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00019824 File Offset: 0x00017A24
		public virtual void Visit(AlterEndpointStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00019835 File Offset: 0x00017A35
		public virtual void ExplicitVisit(AlterEndpointStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00019862 File Offset: 0x00017A62
		public virtual void Visit(AlterCreateEndpointStatementBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00019873 File Offset: 0x00017A73
		public virtual void ExplicitVisit(AlterCreateEndpointStatementBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00019899 File Offset: 0x00017A99
		public virtual void Visit(EndpointAffinity node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000198AA File Offset: 0x00017AAA
		public virtual void ExplicitVisit(EndpointAffinity node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000198C9 File Offset: 0x00017AC9
		public virtual void Visit(EndpointProtocolOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000198DA File Offset: 0x00017ADA
		public virtual void ExplicitVisit(EndpointProtocolOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000198F9 File Offset: 0x00017AF9
		public virtual void Visit(LiteralEndpointProtocolOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001990A File Offset: 0x00017B0A
		public virtual void ExplicitVisit(LiteralEndpointProtocolOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00019930 File Offset: 0x00017B30
		public virtual void Visit(AuthenticationEndpointProtocolOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00019941 File Offset: 0x00017B41
		public virtual void ExplicitVisit(AuthenticationEndpointProtocolOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00019967 File Offset: 0x00017B67
		public virtual void Visit(PortsEndpointProtocolOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00019978 File Offset: 0x00017B78
		public virtual void ExplicitVisit(PortsEndpointProtocolOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001999E File Offset: 0x00017B9E
		public virtual void Visit(CompressionEndpointProtocolOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000199AF File Offset: 0x00017BAF
		public virtual void ExplicitVisit(CompressionEndpointProtocolOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000199D5 File Offset: 0x00017BD5
		public virtual void Visit(ListenerIPEndpointProtocolOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000199E6 File Offset: 0x00017BE6
		public virtual void ExplicitVisit(ListenerIPEndpointProtocolOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00019A0C File Offset: 0x00017C0C
		public virtual void Visit(IPv4 node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00019A1D File Offset: 0x00017C1D
		public virtual void ExplicitVisit(IPv4 node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00019A3C File Offset: 0x00017C3C
		public virtual void Visit(SoapMethod node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00019A4D File Offset: 0x00017C4D
		public virtual void ExplicitVisit(SoapMethod node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00019A73 File Offset: 0x00017C73
		public virtual void Visit(PayloadOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00019A84 File Offset: 0x00017C84
		public virtual void ExplicitVisit(PayloadOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00019AA3 File Offset: 0x00017CA3
		public virtual void Visit(EnabledDisabledPayloadOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00019AB4 File Offset: 0x00017CB4
		public virtual void ExplicitVisit(EnabledDisabledPayloadOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00019ADA File Offset: 0x00017CDA
		public virtual void Visit(WsdlPayloadOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00019AEB File Offset: 0x00017CEB
		public virtual void ExplicitVisit(WsdlPayloadOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00019B11 File Offset: 0x00017D11
		public virtual void Visit(LoginTypePayloadOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00019B22 File Offset: 0x00017D22
		public virtual void ExplicitVisit(LoginTypePayloadOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00019B48 File Offset: 0x00017D48
		public virtual void Visit(LiteralPayloadOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00019B59 File Offset: 0x00017D59
		public virtual void ExplicitVisit(LiteralPayloadOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00019B7F File Offset: 0x00017D7F
		public virtual void Visit(SessionTimeoutPayloadOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00019B90 File Offset: 0x00017D90
		public virtual void ExplicitVisit(SessionTimeoutPayloadOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00019BB6 File Offset: 0x00017DB6
		public virtual void Visit(SchemaPayloadOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00019BC7 File Offset: 0x00017DC7
		public virtual void ExplicitVisit(SchemaPayloadOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00019BED File Offset: 0x00017DED
		public virtual void Visit(CharacterSetPayloadOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00019BFE File Offset: 0x00017DFE
		public virtual void ExplicitVisit(CharacterSetPayloadOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00019C24 File Offset: 0x00017E24
		public virtual void Visit(RolePayloadOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00019C35 File Offset: 0x00017E35
		public virtual void ExplicitVisit(RolePayloadOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00019C5B File Offset: 0x00017E5B
		public virtual void Visit(AuthenticationPayloadOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00019C6C File Offset: 0x00017E6C
		public virtual void ExplicitVisit(AuthenticationPayloadOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00019C92 File Offset: 0x00017E92
		public virtual void Visit(EncryptionPayloadOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00019CA3 File Offset: 0x00017EA3
		public virtual void ExplicitVisit(EncryptionPayloadOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00019CC9 File Offset: 0x00017EC9
		public virtual void Visit(SymmetricKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00019CDA File Offset: 0x00017EDA
		public virtual void ExplicitVisit(SymmetricKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00019D00 File Offset: 0x00017F00
		public virtual void Visit(CreateSymmetricKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00019D11 File Offset: 0x00017F11
		public virtual void ExplicitVisit(CreateSymmetricKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00019D3E File Offset: 0x00017F3E
		public virtual void Visit(KeyOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00019D4F File Offset: 0x00017F4F
		public virtual void ExplicitVisit(KeyOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00019D6E File Offset: 0x00017F6E
		public virtual void Visit(KeySourceKeyOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00019D7F File Offset: 0x00017F7F
		public virtual void ExplicitVisit(KeySourceKeyOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00019DA5 File Offset: 0x00017FA5
		public virtual void Visit(AlgorithmKeyOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00019DB6 File Offset: 0x00017FB6
		public virtual void ExplicitVisit(AlgorithmKeyOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00019DDC File Offset: 0x00017FDC
		public virtual void Visit(IdentityValueKeyOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00019DED File Offset: 0x00017FED
		public virtual void ExplicitVisit(IdentityValueKeyOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00019E13 File Offset: 0x00018013
		public virtual void Visit(ProviderKeyNameKeyOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00019E24 File Offset: 0x00018024
		public virtual void ExplicitVisit(ProviderKeyNameKeyOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00019E4A File Offset: 0x0001804A
		public virtual void Visit(CreationDispositionKeyOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00019E5B File Offset: 0x0001805B
		public virtual void ExplicitVisit(CreationDispositionKeyOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00019E81 File Offset: 0x00018081
		public virtual void Visit(AlterSymmetricKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00019E92 File Offset: 0x00018092
		public virtual void ExplicitVisit(AlterSymmetricKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00019EBF File Offset: 0x000180BF
		public virtual void Visit(FullTextCatalogStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00019ED0 File Offset: 0x000180D0
		public virtual void ExplicitVisit(FullTextCatalogStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00019EF6 File Offset: 0x000180F6
		public virtual void Visit(FullTextCatalogOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00019F07 File Offset: 0x00018107
		public virtual void ExplicitVisit(FullTextCatalogOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00019F26 File Offset: 0x00018126
		public virtual void Visit(OnOffFullTextCatalogOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00019F37 File Offset: 0x00018137
		public virtual void ExplicitVisit(OnOffFullTextCatalogOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00019F5D File Offset: 0x0001815D
		public virtual void Visit(CreateFullTextCatalogStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00019F6E File Offset: 0x0001816E
		public virtual void ExplicitVisit(CreateFullTextCatalogStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00019F9B File Offset: 0x0001819B
		public virtual void Visit(AlterFullTextCatalogStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00019FAC File Offset: 0x000181AC
		public virtual void ExplicitVisit(AlterFullTextCatalogStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00019FD9 File Offset: 0x000181D9
		public virtual void Visit(AlterCreateServiceStatementBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00019FEA File Offset: 0x000181EA
		public virtual void ExplicitVisit(AlterCreateServiceStatementBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001A010 File Offset: 0x00018210
		public virtual void Visit(CreateServiceStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001A021 File Offset: 0x00018221
		public virtual void ExplicitVisit(CreateServiceStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001A04E File Offset: 0x0001824E
		public virtual void Visit(AlterServiceStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001A05F File Offset: 0x0001825F
		public virtual void ExplicitVisit(AlterServiceStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001A08C File Offset: 0x0001828C
		public virtual void Visit(ServiceContract node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001A09D File Offset: 0x0001829D
		public virtual void ExplicitVisit(ServiceContract node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001A0BC File Offset: 0x000182BC
		public virtual void Visit(BinaryExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001A0CD File Offset: 0x000182CD
		public virtual void ExplicitVisit(BinaryExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001A0F3 File Offset: 0x000182F3
		public virtual void Visit(BuiltInFunctionTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001A104 File Offset: 0x00018304
		public virtual void ExplicitVisit(BuiltInFunctionTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001A131 File Offset: 0x00018331
		public virtual void Visit(ComputeClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001A142 File Offset: 0x00018342
		public virtual void ExplicitVisit(ComputeClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001A161 File Offset: 0x00018361
		public virtual void Visit(ComputeFunction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001A172 File Offset: 0x00018372
		public virtual void ExplicitVisit(ComputeFunction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001A191 File Offset: 0x00018391
		public virtual void Visit(PivotedTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001A1A2 File Offset: 0x000183A2
		public virtual void ExplicitVisit(PivotedTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001A1CF File Offset: 0x000183CF
		public virtual void Visit(UnpivotedTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001A1E0 File Offset: 0x000183E0
		public virtual void ExplicitVisit(UnpivotedTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001A20D File Offset: 0x0001840D
		public virtual void Visit(UnqualifiedJoin node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001A21E File Offset: 0x0001841E
		public virtual void ExplicitVisit(UnqualifiedJoin node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001A24B File Offset: 0x0001844B
		public virtual void Visit(TableSampleClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001A25C File Offset: 0x0001845C
		public virtual void ExplicitVisit(TableSampleClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001A27B File Offset: 0x0001847B
		public virtual void Visit(ScalarExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001A28C File Offset: 0x0001848C
		public virtual void ExplicitVisit(ScalarExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001A2AB File Offset: 0x000184AB
		public virtual void Visit(BooleanExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001A2BC File Offset: 0x000184BC
		public virtual void ExplicitVisit(BooleanExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001A2DB File Offset: 0x000184DB
		public virtual void Visit(BooleanNotExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001A2EC File Offset: 0x000184EC
		public virtual void ExplicitVisit(BooleanNotExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001A312 File Offset: 0x00018512
		public virtual void Visit(BooleanParenthesisExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001A323 File Offset: 0x00018523
		public virtual void ExplicitVisit(BooleanParenthesisExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001A349 File Offset: 0x00018549
		public virtual void Visit(BooleanComparisonExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001A35A File Offset: 0x0001855A
		public virtual void ExplicitVisit(BooleanComparisonExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001A380 File Offset: 0x00018580
		public virtual void Visit(BooleanBinaryExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001A391 File Offset: 0x00018591
		public virtual void ExplicitVisit(BooleanBinaryExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001A3B7 File Offset: 0x000185B7
		public virtual void Visit(BooleanIsNullExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001A3C8 File Offset: 0x000185C8
		public virtual void ExplicitVisit(BooleanIsNullExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001A3EE File Offset: 0x000185EE
		public virtual void Visit(ExpressionWithSortOrder node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001A3FF File Offset: 0x000185FF
		public virtual void ExplicitVisit(ExpressionWithSortOrder node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001A41E File Offset: 0x0001861E
		public virtual void Visit(GroupByClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001A42F File Offset: 0x0001862F
		public virtual void ExplicitVisit(GroupByClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001A44E File Offset: 0x0001864E
		public virtual void Visit(GroupingSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001A45F File Offset: 0x0001865F
		public virtual void ExplicitVisit(GroupingSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001A47E File Offset: 0x0001867E
		public virtual void Visit(ExpressionGroupingSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001A48F File Offset: 0x0001868F
		public virtual void ExplicitVisit(ExpressionGroupingSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001A4B5 File Offset: 0x000186B5
		public virtual void Visit(CompositeGroupingSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001A4C6 File Offset: 0x000186C6
		public virtual void ExplicitVisit(CompositeGroupingSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001A4EC File Offset: 0x000186EC
		public virtual void Visit(CubeGroupingSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001A4FD File Offset: 0x000186FD
		public virtual void ExplicitVisit(CubeGroupingSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001A523 File Offset: 0x00018723
		public virtual void Visit(RollupGroupingSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001A534 File Offset: 0x00018734
		public virtual void ExplicitVisit(RollupGroupingSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001A55A File Offset: 0x0001875A
		public virtual void Visit(GrandTotalGroupingSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001A56B File Offset: 0x0001876B
		public virtual void ExplicitVisit(GrandTotalGroupingSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001A591 File Offset: 0x00018791
		public virtual void Visit(GroupingSetsGroupingSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001A5A2 File Offset: 0x000187A2
		public virtual void ExplicitVisit(GroupingSetsGroupingSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001A5C8 File Offset: 0x000187C8
		public virtual void Visit(OutputClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001A5D9 File Offset: 0x000187D9
		public virtual void ExplicitVisit(OutputClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001A5F8 File Offset: 0x000187F8
		public virtual void Visit(OutputIntoClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001A609 File Offset: 0x00018809
		public virtual void ExplicitVisit(OutputIntoClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001A628 File Offset: 0x00018828
		public virtual void Visit(HavingClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001A639 File Offset: 0x00018839
		public virtual void ExplicitVisit(HavingClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001A658 File Offset: 0x00018858
		public virtual void Visit(IdentityFunctionCall node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001A669 File Offset: 0x00018869
		public virtual void ExplicitVisit(IdentityFunctionCall node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001A68F File Offset: 0x0001888F
		public virtual void Visit(JoinParenthesisTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001A6A0 File Offset: 0x000188A0
		public virtual void ExplicitVisit(JoinParenthesisTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001A6C6 File Offset: 0x000188C6
		public virtual void Visit(OrderByClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001A6D7 File Offset: 0x000188D7
		public virtual void ExplicitVisit(OrderByClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001A6F6 File Offset: 0x000188F6
		public virtual void Visit(JoinTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001A707 File Offset: 0x00018907
		public virtual void ExplicitVisit(JoinTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001A72D File Offset: 0x0001892D
		public virtual void Visit(QualifiedJoin node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001A73E File Offset: 0x0001893E
		public virtual void ExplicitVisit(QualifiedJoin node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001A76B File Offset: 0x0001896B
		public virtual void Visit(OdbcQualifiedJoinTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001A77C File Offset: 0x0001897C
		public virtual void ExplicitVisit(OdbcQualifiedJoinTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001A7A2 File Offset: 0x000189A2
		public virtual void Visit(QueryExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001A7B3 File Offset: 0x000189B3
		public virtual void ExplicitVisit(QueryExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001A7D2 File Offset: 0x000189D2
		public virtual void Visit(QueryParenthesisExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001A7E3 File Offset: 0x000189E3
		public virtual void ExplicitVisit(QueryParenthesisExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001A809 File Offset: 0x00018A09
		public virtual void Visit(QuerySpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001A81A File Offset: 0x00018A1A
		public virtual void ExplicitVisit(QuerySpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001A840 File Offset: 0x00018A40
		public virtual void Visit(FromClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001A851 File Offset: 0x00018A51
		public virtual void ExplicitVisit(FromClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001A870 File Offset: 0x00018A70
		public virtual void Visit(SelectElement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001A881 File Offset: 0x00018A81
		public virtual void ExplicitVisit(SelectElement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001A8A0 File Offset: 0x00018AA0
		public virtual void Visit(SelectScalarExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001A8B1 File Offset: 0x00018AB1
		public virtual void ExplicitVisit(SelectScalarExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001A8D7 File Offset: 0x00018AD7
		public virtual void Visit(SelectStarExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		public virtual void ExplicitVisit(SelectStarExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001A90E File Offset: 0x00018B0E
		public virtual void Visit(SelectSetVariable node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001A91F File Offset: 0x00018B1F
		public virtual void ExplicitVisit(SelectSetVariable node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001A945 File Offset: 0x00018B45
		public virtual void Visit(TableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001A956 File Offset: 0x00018B56
		public virtual void ExplicitVisit(TableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001A975 File Offset: 0x00018B75
		public virtual void Visit(TableReferenceWithAlias node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001A986 File Offset: 0x00018B86
		public virtual void ExplicitVisit(TableReferenceWithAlias node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001A9AC File Offset: 0x00018BAC
		public virtual void Visit(TableReferenceWithAliasAndColumns node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001A9BD File Offset: 0x00018BBD
		public virtual void ExplicitVisit(TableReferenceWithAliasAndColumns node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001A9EA File Offset: 0x00018BEA
		public virtual void Visit(DataModificationTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001A9FB File Offset: 0x00018BFB
		public virtual void ExplicitVisit(DataModificationTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001AA2F File Offset: 0x00018C2F
		public virtual void Visit(ChangeTableChangesTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001AA40 File Offset: 0x00018C40
		public virtual void ExplicitVisit(ChangeTableChangesTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001AA74 File Offset: 0x00018C74
		public virtual void Visit(ChangeTableVersionTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001AA85 File Offset: 0x00018C85
		public virtual void ExplicitVisit(ChangeTableVersionTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001AAB9 File Offset: 0x00018CB9
		public virtual void Visit(BooleanTernaryExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001AACA File Offset: 0x00018CCA
		public virtual void ExplicitVisit(BooleanTernaryExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001AAF0 File Offset: 0x00018CF0
		public virtual void Visit(TopRowFilter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001AB01 File Offset: 0x00018D01
		public virtual void ExplicitVisit(TopRowFilter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001AB20 File Offset: 0x00018D20
		public virtual void Visit(OffsetClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001AB31 File Offset: 0x00018D31
		public virtual void ExplicitVisit(OffsetClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001AB50 File Offset: 0x00018D50
		public virtual void Visit(UnaryExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001AB61 File Offset: 0x00018D61
		public virtual void ExplicitVisit(UnaryExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001AB87 File Offset: 0x00018D87
		public virtual void Visit(BinaryQueryExpression node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001AB98 File Offset: 0x00018D98
		public virtual void ExplicitVisit(BinaryQueryExpression node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001ABBE File Offset: 0x00018DBE
		public virtual void Visit(VariableTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001ABCF File Offset: 0x00018DCF
		public virtual void ExplicitVisit(VariableTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001ABFC File Offset: 0x00018DFC
		public virtual void Visit(VariableMethodCallTableReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001AC0D File Offset: 0x00018E0D
		public virtual void ExplicitVisit(VariableMethodCallTableReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001AC41 File Offset: 0x00018E41
		public virtual void Visit(DropPartitionFunctionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001AC52 File Offset: 0x00018E52
		public virtual void ExplicitVisit(DropPartitionFunctionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001AC7F File Offset: 0x00018E7F
		public virtual void Visit(DropPartitionSchemeStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001AC90 File Offset: 0x00018E90
		public virtual void ExplicitVisit(DropPartitionSchemeStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001ACBD File Offset: 0x00018EBD
		public virtual void Visit(DropSynonymStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001ACCE File Offset: 0x00018ECE
		public virtual void ExplicitVisit(DropSynonymStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001ACFB File Offset: 0x00018EFB
		public virtual void Visit(DropAggregateStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001AD0C File Offset: 0x00018F0C
		public virtual void ExplicitVisit(DropAggregateStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001AD39 File Offset: 0x00018F39
		public virtual void Visit(DropAssemblyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001AD4A File Offset: 0x00018F4A
		public virtual void ExplicitVisit(DropAssemblyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001AD77 File Offset: 0x00018F77
		public virtual void Visit(DropApplicationRoleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001AD88 File Offset: 0x00018F88
		public virtual void ExplicitVisit(DropApplicationRoleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001ADB5 File Offset: 0x00018FB5
		public virtual void Visit(DropFullTextCatalogStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001ADC6 File Offset: 0x00018FC6
		public virtual void ExplicitVisit(DropFullTextCatalogStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001ADF3 File Offset: 0x00018FF3
		public virtual void Visit(DropFullTextIndexStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001AE04 File Offset: 0x00019004
		public virtual void ExplicitVisit(DropFullTextIndexStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001AE2A File Offset: 0x0001902A
		public virtual void Visit(DropLoginStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001AE3B File Offset: 0x0001903B
		public virtual void ExplicitVisit(DropLoginStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001AE68 File Offset: 0x00019068
		public virtual void Visit(DropRoleStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001AE79 File Offset: 0x00019079
		public virtual void ExplicitVisit(DropRoleStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001AEA6 File Offset: 0x000190A6
		public virtual void Visit(DropTypeStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001AEB7 File Offset: 0x000190B7
		public virtual void ExplicitVisit(DropTypeStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001AEDD File Offset: 0x000190DD
		public virtual void Visit(DropUserStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001AEEE File Offset: 0x000190EE
		public virtual void ExplicitVisit(DropUserStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001AF1B File Offset: 0x0001911B
		public virtual void Visit(DropMasterKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001AF2C File Offset: 0x0001912C
		public virtual void ExplicitVisit(DropMasterKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001AF52 File Offset: 0x00019152
		public virtual void Visit(DropSymmetricKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001AF63 File Offset: 0x00019163
		public virtual void ExplicitVisit(DropSymmetricKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001AF90 File Offset: 0x00019190
		public virtual void Visit(DropAsymmetricKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001AFA1 File Offset: 0x000191A1
		public virtual void ExplicitVisit(DropAsymmetricKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001AFCE File Offset: 0x000191CE
		public virtual void Visit(DropCertificateStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001AFDF File Offset: 0x000191DF
		public virtual void ExplicitVisit(DropCertificateStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001B00C File Offset: 0x0001920C
		public virtual void Visit(DropCredentialStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001B01D File Offset: 0x0001921D
		public virtual void ExplicitVisit(DropCredentialStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001B04A File Offset: 0x0001924A
		public virtual void Visit(AlterPartitionFunctionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001B05B File Offset: 0x0001925B
		public virtual void ExplicitVisit(AlterPartitionFunctionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001B081 File Offset: 0x00019281
		public virtual void Visit(AlterPartitionSchemeStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001B092 File Offset: 0x00019292
		public virtual void ExplicitVisit(AlterPartitionSchemeStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001B0B8 File Offset: 0x000192B8
		public virtual void Visit(AlterFullTextIndexStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001B0C9 File Offset: 0x000192C9
		public virtual void ExplicitVisit(AlterFullTextIndexStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001B0EF File Offset: 0x000192EF
		public virtual void Visit(AlterFullTextIndexAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001B100 File Offset: 0x00019300
		public virtual void ExplicitVisit(AlterFullTextIndexAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001B11F File Offset: 0x0001931F
		public virtual void Visit(SimpleAlterFullTextIndexAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001B130 File Offset: 0x00019330
		public virtual void ExplicitVisit(SimpleAlterFullTextIndexAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001B156 File Offset: 0x00019356
		public virtual void Visit(SetStopListAlterFullTextIndexAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001B167 File Offset: 0x00019367
		public virtual void ExplicitVisit(SetStopListAlterFullTextIndexAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001B18D File Offset: 0x0001938D
		public virtual void Visit(SetSearchPropertyListAlterFullTextIndexAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001B19E File Offset: 0x0001939E
		public virtual void ExplicitVisit(SetSearchPropertyListAlterFullTextIndexAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001B1C4 File Offset: 0x000193C4
		public virtual void Visit(DropAlterFullTextIndexAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001B1D5 File Offset: 0x000193D5
		public virtual void ExplicitVisit(DropAlterFullTextIndexAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001B1FB File Offset: 0x000193FB
		public virtual void Visit(AddAlterFullTextIndexAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001B20C File Offset: 0x0001940C
		public virtual void ExplicitVisit(AddAlterFullTextIndexAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001B232 File Offset: 0x00019432
		public virtual void Visit(AlterColumnAlterFullTextIndexAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001B243 File Offset: 0x00019443
		public virtual void ExplicitVisit(AlterColumnAlterFullTextIndexAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001B269 File Offset: 0x00019469
		public virtual void Visit(CreateSearchPropertyListStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001B27A File Offset: 0x0001947A
		public virtual void ExplicitVisit(CreateSearchPropertyListStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001B2A0 File Offset: 0x000194A0
		public virtual void Visit(AlterSearchPropertyListStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001B2B1 File Offset: 0x000194B1
		public virtual void ExplicitVisit(AlterSearchPropertyListStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001B2D7 File Offset: 0x000194D7
		public virtual void Visit(SearchPropertyListAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001B2E8 File Offset: 0x000194E8
		public virtual void ExplicitVisit(SearchPropertyListAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001B307 File Offset: 0x00019507
		public virtual void Visit(AddSearchPropertyListAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001B318 File Offset: 0x00019518
		public virtual void ExplicitVisit(AddSearchPropertyListAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001B33E File Offset: 0x0001953E
		public virtual void Visit(DropSearchPropertyListAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001B34F File Offset: 0x0001954F
		public virtual void ExplicitVisit(DropSearchPropertyListAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001B375 File Offset: 0x00019575
		public virtual void Visit(DropSearchPropertyListStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001B386 File Offset: 0x00019586
		public virtual void ExplicitVisit(DropSearchPropertyListStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001B3B3 File Offset: 0x000195B3
		public virtual void Visit(CreateLoginStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001B3C4 File Offset: 0x000195C4
		public virtual void ExplicitVisit(CreateLoginStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001B3EA File Offset: 0x000195EA
		public virtual void Visit(CreateLoginSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001B3FB File Offset: 0x000195FB
		public virtual void ExplicitVisit(CreateLoginSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001B41A File Offset: 0x0001961A
		public virtual void Visit(PasswordCreateLoginSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001B42B File Offset: 0x0001962B
		public virtual void ExplicitVisit(PasswordCreateLoginSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001B451 File Offset: 0x00019651
		public virtual void Visit(PrincipalOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001B462 File Offset: 0x00019662
		public virtual void ExplicitVisit(PrincipalOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001B481 File Offset: 0x00019681
		public virtual void Visit(OnOffPrincipalOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001B492 File Offset: 0x00019692
		public virtual void ExplicitVisit(OnOffPrincipalOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001B4B8 File Offset: 0x000196B8
		public virtual void Visit(LiteralPrincipalOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001B4C9 File Offset: 0x000196C9
		public virtual void ExplicitVisit(LiteralPrincipalOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001B4EF File Offset: 0x000196EF
		public virtual void Visit(IdentifierPrincipalOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001B500 File Offset: 0x00019700
		public virtual void ExplicitVisit(IdentifierPrincipalOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001B526 File Offset: 0x00019726
		public virtual void Visit(WindowsCreateLoginSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001B537 File Offset: 0x00019737
		public virtual void ExplicitVisit(WindowsCreateLoginSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001B55D File Offset: 0x0001975D
		public virtual void Visit(CertificateCreateLoginSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001B56E File Offset: 0x0001976E
		public virtual void ExplicitVisit(CertificateCreateLoginSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001B594 File Offset: 0x00019794
		public virtual void Visit(AsymmetricKeyCreateLoginSource node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001B5A5 File Offset: 0x000197A5
		public virtual void ExplicitVisit(AsymmetricKeyCreateLoginSource node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001B5CB File Offset: 0x000197CB
		public virtual void Visit(PasswordAlterPrincipalOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001B5DC File Offset: 0x000197DC
		public virtual void ExplicitVisit(PasswordAlterPrincipalOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001B602 File Offset: 0x00019802
		public virtual void Visit(AlterLoginStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001B613 File Offset: 0x00019813
		public virtual void ExplicitVisit(AlterLoginStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001B639 File Offset: 0x00019839
		public virtual void Visit(AlterLoginOptionsStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001B64A File Offset: 0x0001984A
		public virtual void ExplicitVisit(AlterLoginOptionsStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001B677 File Offset: 0x00019877
		public virtual void Visit(AlterLoginEnableDisableStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001B688 File Offset: 0x00019888
		public virtual void ExplicitVisit(AlterLoginEnableDisableStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001B6B5 File Offset: 0x000198B5
		public virtual void Visit(AlterLoginAddDropCredentialStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001B6C6 File Offset: 0x000198C6
		public virtual void ExplicitVisit(AlterLoginAddDropCredentialStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001B6F3 File Offset: 0x000198F3
		public virtual void Visit(RevertStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001B704 File Offset: 0x00019904
		public virtual void ExplicitVisit(RevertStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001B72A File Offset: 0x0001992A
		public virtual void Visit(DropContractStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001B73B File Offset: 0x0001993B
		public virtual void ExplicitVisit(DropContractStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001B768 File Offset: 0x00019968
		public virtual void Visit(DropEndpointStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001B779 File Offset: 0x00019979
		public virtual void ExplicitVisit(DropEndpointStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001B7A6 File Offset: 0x000199A6
		public virtual void Visit(DropMessageTypeStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001B7B7 File Offset: 0x000199B7
		public virtual void ExplicitVisit(DropMessageTypeStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001B7E4 File Offset: 0x000199E4
		public virtual void Visit(DropQueueStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001B7F5 File Offset: 0x000199F5
		public virtual void ExplicitVisit(DropQueueStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001B81B File Offset: 0x00019A1B
		public virtual void Visit(DropRemoteServiceBindingStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001B82C File Offset: 0x00019A2C
		public virtual void ExplicitVisit(DropRemoteServiceBindingStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001B859 File Offset: 0x00019A59
		public virtual void Visit(DropRouteStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001B86A File Offset: 0x00019A6A
		public virtual void ExplicitVisit(DropRouteStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001B897 File Offset: 0x00019A97
		public virtual void Visit(DropServiceStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001B8A8 File Offset: 0x00019AA8
		public virtual void ExplicitVisit(DropServiceStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001B8D5 File Offset: 0x00019AD5
		public virtual void Visit(SignatureStatementBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001B8E6 File Offset: 0x00019AE6
		public virtual void ExplicitVisit(SignatureStatementBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001B90C File Offset: 0x00019B0C
		public virtual void Visit(AddSignatureStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001B91D File Offset: 0x00019B1D
		public virtual void ExplicitVisit(AddSignatureStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001B94A File Offset: 0x00019B4A
		public virtual void Visit(DropSignatureStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001B95B File Offset: 0x00019B5B
		public virtual void ExplicitVisit(DropSignatureStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001B988 File Offset: 0x00019B88
		public virtual void Visit(DropEventNotificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001B999 File Offset: 0x00019B99
		public virtual void ExplicitVisit(DropEventNotificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001B9BF File Offset: 0x00019BBF
		public virtual void Visit(ExecuteAsStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001B9D0 File Offset: 0x00019BD0
		public virtual void ExplicitVisit(ExecuteAsStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001B9F6 File Offset: 0x00019BF6
		public virtual void Visit(EndConversationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001BA07 File Offset: 0x00019C07
		public virtual void ExplicitVisit(EndConversationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001BA2D File Offset: 0x00019C2D
		public virtual void Visit(MoveConversationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001BA3E File Offset: 0x00019C3E
		public virtual void ExplicitVisit(MoveConversationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001BA64 File Offset: 0x00019C64
		public virtual void Visit(GetConversationGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001BA75 File Offset: 0x00019C75
		public virtual void ExplicitVisit(GetConversationGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001BAA2 File Offset: 0x00019CA2
		public virtual void Visit(ReceiveStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001BAB3 File Offset: 0x00019CB3
		public virtual void ExplicitVisit(ReceiveStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001BAE0 File Offset: 0x00019CE0
		public virtual void Visit(SendStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001BAF1 File Offset: 0x00019CF1
		public virtual void ExplicitVisit(SendStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001BB17 File Offset: 0x00019D17
		public virtual void Visit(WaitForSupportedStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001BB28 File Offset: 0x00019D28
		public virtual void ExplicitVisit(WaitForSupportedStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001BB4E File Offset: 0x00019D4E
		public virtual void Visit(AlterSchemaStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001BB5F File Offset: 0x00019D5F
		public virtual void ExplicitVisit(AlterSchemaStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001BB85 File Offset: 0x00019D85
		public virtual void Visit(AlterAsymmetricKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001BB96 File Offset: 0x00019D96
		public virtual void ExplicitVisit(AlterAsymmetricKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001BBBC File Offset: 0x00019DBC
		public virtual void Visit(AlterServiceMasterKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001BBCD File Offset: 0x00019DCD
		public virtual void ExplicitVisit(AlterServiceMasterKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001BBF3 File Offset: 0x00019DF3
		public virtual void Visit(BeginConversationTimerStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001BC04 File Offset: 0x00019E04
		public virtual void ExplicitVisit(BeginConversationTimerStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001BC2A File Offset: 0x00019E2A
		public virtual void Visit(BeginDialogStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001BC3B File Offset: 0x00019E3B
		public virtual void ExplicitVisit(BeginDialogStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001BC61 File Offset: 0x00019E61
		public virtual void Visit(DialogOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001BC72 File Offset: 0x00019E72
		public virtual void ExplicitVisit(DialogOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001BC91 File Offset: 0x00019E91
		public virtual void Visit(ScalarExpressionDialogOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001BCA2 File Offset: 0x00019EA2
		public virtual void ExplicitVisit(ScalarExpressionDialogOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001BCC8 File Offset: 0x00019EC8
		public virtual void Visit(OnOffDialogOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001BCD9 File Offset: 0x00019ED9
		public virtual void ExplicitVisit(OnOffDialogOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001BCFF File Offset: 0x00019EFF
		public virtual void Visit(BackupCertificateStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001BD10 File Offset: 0x00019F10
		public virtual void ExplicitVisit(BackupCertificateStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001BD3D File Offset: 0x00019F3D
		public virtual void Visit(BackupRestoreMasterKeyStatementBase node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001BD4E File Offset: 0x00019F4E
		public virtual void ExplicitVisit(BackupRestoreMasterKeyStatementBase node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001BD74 File Offset: 0x00019F74
		public virtual void Visit(BackupServiceMasterKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001BD85 File Offset: 0x00019F85
		public virtual void ExplicitVisit(BackupServiceMasterKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001BDB2 File Offset: 0x00019FB2
		public virtual void Visit(RestoreServiceMasterKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001BDC3 File Offset: 0x00019FC3
		public virtual void ExplicitVisit(RestoreServiceMasterKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001BDF0 File Offset: 0x00019FF0
		public virtual void Visit(BackupMasterKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001BE01 File Offset: 0x0001A001
		public virtual void ExplicitVisit(BackupMasterKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001BE2E File Offset: 0x0001A02E
		public virtual void Visit(RestoreMasterKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001BE3F File Offset: 0x0001A03F
		public virtual void ExplicitVisit(RestoreMasterKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001BE6C File Offset: 0x0001A06C
		public virtual void Visit(ScalarExpressionSnippet node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001BE7D File Offset: 0x0001A07D
		public virtual void ExplicitVisit(ScalarExpressionSnippet node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001BEA3 File Offset: 0x0001A0A3
		public virtual void Visit(BooleanExpressionSnippet node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001BEB4 File Offset: 0x0001A0B4
		public virtual void ExplicitVisit(BooleanExpressionSnippet node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001BEDA File Offset: 0x0001A0DA
		public virtual void Visit(StatementListSnippet node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001BEEB File Offset: 0x0001A0EB
		public virtual void ExplicitVisit(StatementListSnippet node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001BF11 File Offset: 0x0001A111
		public virtual void Visit(SelectStatementSnippet node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001BF22 File Offset: 0x0001A122
		public virtual void ExplicitVisit(SelectStatementSnippet node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001BF56 File Offset: 0x0001A156
		public virtual void Visit(SchemaObjectNameSnippet node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001BF67 File Offset: 0x0001A167
		public virtual void ExplicitVisit(SchemaObjectNameSnippet node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001BF94 File Offset: 0x0001A194
		public virtual void Visit(TSqlFragmentSnippet node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001BFA5 File Offset: 0x0001A1A5
		public virtual void ExplicitVisit(TSqlFragmentSnippet node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		public virtual void Visit(TSqlStatementSnippet node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001BFD5 File Offset: 0x0001A1D5
		public virtual void ExplicitVisit(TSqlStatementSnippet node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001BFFB File Offset: 0x0001A1FB
		public virtual void Visit(IdentifierSnippet node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001C00C File Offset: 0x0001A20C
		public virtual void ExplicitVisit(IdentifierSnippet node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001C032 File Offset: 0x0001A232
		public virtual void Visit(TSqlScript node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001C043 File Offset: 0x0001A243
		public virtual void ExplicitVisit(TSqlScript node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001C062 File Offset: 0x0001A262
		public virtual void Visit(TSqlBatch node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001C073 File Offset: 0x0001A273
		public virtual void ExplicitVisit(TSqlBatch node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001C092 File Offset: 0x0001A292
		public virtual void Visit(TSqlStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001C0A3 File Offset: 0x0001A2A3
		public virtual void ExplicitVisit(TSqlStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001C0C2 File Offset: 0x0001A2C2
		public virtual void Visit(DataModificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001C0D3 File Offset: 0x0001A2D3
		public virtual void ExplicitVisit(DataModificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001C100 File Offset: 0x0001A300
		public virtual void Visit(DataModificationSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001C111 File Offset: 0x0001A311
		public virtual void ExplicitVisit(DataModificationSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001C130 File Offset: 0x0001A330
		public virtual void Visit(MergeStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001C141 File Offset: 0x0001A341
		public virtual void ExplicitVisit(MergeStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001C175 File Offset: 0x0001A375
		public virtual void Visit(MergeSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001C186 File Offset: 0x0001A386
		public virtual void ExplicitVisit(MergeSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001C1AC File Offset: 0x0001A3AC
		public virtual void Visit(MergeActionClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001C1BD File Offset: 0x0001A3BD
		public virtual void ExplicitVisit(MergeActionClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001C1DC File Offset: 0x0001A3DC
		public virtual void Visit(MergeAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001C1ED File Offset: 0x0001A3ED
		public virtual void ExplicitVisit(MergeAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001C20C File Offset: 0x0001A40C
		public virtual void Visit(UpdateMergeAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001C21D File Offset: 0x0001A41D
		public virtual void ExplicitVisit(UpdateMergeAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001C243 File Offset: 0x0001A443
		public virtual void Visit(DeleteMergeAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001C254 File Offset: 0x0001A454
		public virtual void ExplicitVisit(DeleteMergeAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001C27A File Offset: 0x0001A47A
		public virtual void Visit(InsertMergeAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001C28B File Offset: 0x0001A48B
		public virtual void ExplicitVisit(InsertMergeAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001C2B1 File Offset: 0x0001A4B1
		public virtual void Visit(CreateTypeTableStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001C2C2 File Offset: 0x0001A4C2
		public virtual void ExplicitVisit(CreateTypeTableStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001C2EF File Offset: 0x0001A4EF
		public virtual void Visit(AuditSpecificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001C300 File Offset: 0x0001A500
		public virtual void ExplicitVisit(AuditSpecificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001C326 File Offset: 0x0001A526
		public virtual void Visit(AuditSpecificationPart node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001C337 File Offset: 0x0001A537
		public virtual void ExplicitVisit(AuditSpecificationPart node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001C356 File Offset: 0x0001A556
		public virtual void Visit(AuditSpecificationDetail node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001C367 File Offset: 0x0001A567
		public virtual void ExplicitVisit(AuditSpecificationDetail node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001C386 File Offset: 0x0001A586
		public virtual void Visit(AuditActionSpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001C397 File Offset: 0x0001A597
		public virtual void ExplicitVisit(AuditActionSpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001C3BD File Offset: 0x0001A5BD
		public virtual void Visit(DatabaseAuditAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001C3CE File Offset: 0x0001A5CE
		public virtual void ExplicitVisit(DatabaseAuditAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001C3ED File Offset: 0x0001A5ED
		public virtual void Visit(AuditActionGroupReference node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001C3FE File Offset: 0x0001A5FE
		public virtual void ExplicitVisit(AuditActionGroupReference node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001C424 File Offset: 0x0001A624
		public virtual void Visit(CreateDatabaseAuditSpecificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001C435 File Offset: 0x0001A635
		public virtual void ExplicitVisit(CreateDatabaseAuditSpecificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001C462 File Offset: 0x0001A662
		public virtual void Visit(AlterDatabaseAuditSpecificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001C473 File Offset: 0x0001A673
		public virtual void ExplicitVisit(AlterDatabaseAuditSpecificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001C4A0 File Offset: 0x0001A6A0
		public virtual void Visit(DropDatabaseAuditSpecificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001C4B1 File Offset: 0x0001A6B1
		public virtual void ExplicitVisit(DropDatabaseAuditSpecificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001C4DE File Offset: 0x0001A6DE
		public virtual void Visit(CreateServerAuditSpecificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001C4EF File Offset: 0x0001A6EF
		public virtual void ExplicitVisit(CreateServerAuditSpecificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001C51C File Offset: 0x0001A71C
		public virtual void Visit(AlterServerAuditSpecificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001C52D File Offset: 0x0001A72D
		public virtual void ExplicitVisit(AlterServerAuditSpecificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001C55A File Offset: 0x0001A75A
		public virtual void Visit(DropServerAuditSpecificationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001C56B File Offset: 0x0001A76B
		public virtual void ExplicitVisit(DropServerAuditSpecificationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001C598 File Offset: 0x0001A798
		public virtual void Visit(ServerAuditStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001C5A9 File Offset: 0x0001A7A9
		public virtual void ExplicitVisit(ServerAuditStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001C5CF File Offset: 0x0001A7CF
		public virtual void Visit(CreateServerAuditStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		public virtual void ExplicitVisit(CreateServerAuditStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001C60D File Offset: 0x0001A80D
		public virtual void Visit(AlterServerAuditStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001C61E File Offset: 0x0001A81E
		public virtual void ExplicitVisit(AlterServerAuditStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001C64B File Offset: 0x0001A84B
		public virtual void Visit(DropServerAuditStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001C65C File Offset: 0x0001A85C
		public virtual void ExplicitVisit(DropServerAuditStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001C689 File Offset: 0x0001A889
		public virtual void Visit(AuditTarget node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001C69A File Offset: 0x0001A89A
		public virtual void ExplicitVisit(AuditTarget node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001C6B9 File Offset: 0x0001A8B9
		public virtual void Visit(AuditOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001C6CA File Offset: 0x0001A8CA
		public virtual void ExplicitVisit(AuditOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001C6E9 File Offset: 0x0001A8E9
		public virtual void Visit(QueueDelayAuditOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001C6FA File Offset: 0x0001A8FA
		public virtual void ExplicitVisit(QueueDelayAuditOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001C720 File Offset: 0x0001A920
		public virtual void Visit(AuditGuidAuditOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001C731 File Offset: 0x0001A931
		public virtual void ExplicitVisit(AuditGuidAuditOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001C757 File Offset: 0x0001A957
		public virtual void Visit(OnFailureAuditOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001C768 File Offset: 0x0001A968
		public virtual void ExplicitVisit(OnFailureAuditOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001C78E File Offset: 0x0001A98E
		public virtual void Visit(StateAuditOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001C79F File Offset: 0x0001A99F
		public virtual void ExplicitVisit(StateAuditOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001C7C5 File Offset: 0x0001A9C5
		public virtual void Visit(AuditTargetOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001C7D6 File Offset: 0x0001A9D6
		public virtual void ExplicitVisit(AuditTargetOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001C7F5 File Offset: 0x0001A9F5
		public virtual void Visit(MaxSizeAuditTargetOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001C806 File Offset: 0x0001AA06
		public virtual void ExplicitVisit(MaxSizeAuditTargetOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001C82C File Offset: 0x0001AA2C
		public virtual void Visit(MaxRolloverFilesAuditTargetOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001C83D File Offset: 0x0001AA3D
		public virtual void ExplicitVisit(MaxRolloverFilesAuditTargetOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001C863 File Offset: 0x0001AA63
		public virtual void Visit(LiteralAuditTargetOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001C874 File Offset: 0x0001AA74
		public virtual void ExplicitVisit(LiteralAuditTargetOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001C89A File Offset: 0x0001AA9A
		public virtual void Visit(OnOffAuditTargetOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001C8AB File Offset: 0x0001AAAB
		public virtual void ExplicitVisit(OnOffAuditTargetOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001C8D1 File Offset: 0x0001AAD1
		public virtual void Visit(DatabaseEncryptionKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001C8E2 File Offset: 0x0001AAE2
		public virtual void ExplicitVisit(DatabaseEncryptionKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001C908 File Offset: 0x0001AB08
		public virtual void Visit(CreateDatabaseEncryptionKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001C919 File Offset: 0x0001AB19
		public virtual void ExplicitVisit(CreateDatabaseEncryptionKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001C946 File Offset: 0x0001AB46
		public virtual void Visit(AlterDatabaseEncryptionKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001C957 File Offset: 0x0001AB57
		public virtual void ExplicitVisit(AlterDatabaseEncryptionKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001C984 File Offset: 0x0001AB84
		public virtual void Visit(DropDatabaseEncryptionKeyStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001C995 File Offset: 0x0001AB95
		public virtual void ExplicitVisit(DropDatabaseEncryptionKeyStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001C9BB File Offset: 0x0001ABBB
		public virtual void Visit(ResourcePoolStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001C9CC File Offset: 0x0001ABCC
		public virtual void ExplicitVisit(ResourcePoolStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001C9F2 File Offset: 0x0001ABF2
		public virtual void Visit(ResourcePoolParameter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001CA03 File Offset: 0x0001AC03
		public virtual void ExplicitVisit(ResourcePoolParameter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001CA22 File Offset: 0x0001AC22
		public virtual void Visit(ResourcePoolAffinitySpecification node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001CA33 File Offset: 0x0001AC33
		public virtual void ExplicitVisit(ResourcePoolAffinitySpecification node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001CA52 File Offset: 0x0001AC52
		public virtual void Visit(CreateResourcePoolStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001CA63 File Offset: 0x0001AC63
		public virtual void ExplicitVisit(CreateResourcePoolStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001CA90 File Offset: 0x0001AC90
		public virtual void Visit(AlterResourcePoolStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001CAA1 File Offset: 0x0001ACA1
		public virtual void ExplicitVisit(AlterResourcePoolStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001CACE File Offset: 0x0001ACCE
		public virtual void Visit(DropResourcePoolStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001CADF File Offset: 0x0001ACDF
		public virtual void ExplicitVisit(DropResourcePoolStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001CB0C File Offset: 0x0001AD0C
		public virtual void Visit(WorkloadGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001CB1D File Offset: 0x0001AD1D
		public virtual void ExplicitVisit(WorkloadGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001CB43 File Offset: 0x0001AD43
		public virtual void Visit(WorkloadGroupResourceParameter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001CB54 File Offset: 0x0001AD54
		public virtual void ExplicitVisit(WorkloadGroupResourceParameter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001CB7A File Offset: 0x0001AD7A
		public virtual void Visit(WorkloadGroupImportanceParameter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001CB8B File Offset: 0x0001AD8B
		public virtual void ExplicitVisit(WorkloadGroupImportanceParameter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001CBB1 File Offset: 0x0001ADB1
		public virtual void Visit(WorkloadGroupParameter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001CBC2 File Offset: 0x0001ADC2
		public virtual void ExplicitVisit(WorkloadGroupParameter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001CBE1 File Offset: 0x0001ADE1
		public virtual void Visit(CreateWorkloadGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001CBF2 File Offset: 0x0001ADF2
		public virtual void ExplicitVisit(CreateWorkloadGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001CC1F File Offset: 0x0001AE1F
		public virtual void Visit(AlterWorkloadGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001CC30 File Offset: 0x0001AE30
		public virtual void ExplicitVisit(AlterWorkloadGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001CC5D File Offset: 0x0001AE5D
		public virtual void Visit(DropWorkloadGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001CC6E File Offset: 0x0001AE6E
		public virtual void ExplicitVisit(DropWorkloadGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001CC9B File Offset: 0x0001AE9B
		public virtual void Visit(BrokerPriorityStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001CCAC File Offset: 0x0001AEAC
		public virtual void ExplicitVisit(BrokerPriorityStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001CCD2 File Offset: 0x0001AED2
		public virtual void Visit(BrokerPriorityParameter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001CCE3 File Offset: 0x0001AEE3
		public virtual void ExplicitVisit(BrokerPriorityParameter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0001CD02 File Offset: 0x0001AF02
		public virtual void Visit(CreateBrokerPriorityStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001CD13 File Offset: 0x0001AF13
		public virtual void ExplicitVisit(CreateBrokerPriorityStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001CD40 File Offset: 0x0001AF40
		public virtual void Visit(AlterBrokerPriorityStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001CD51 File Offset: 0x0001AF51
		public virtual void ExplicitVisit(AlterBrokerPriorityStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001CD7E File Offset: 0x0001AF7E
		public virtual void Visit(DropBrokerPriorityStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001CD8F File Offset: 0x0001AF8F
		public virtual void ExplicitVisit(DropBrokerPriorityStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001CDBC File Offset: 0x0001AFBC
		public virtual void Visit(CreateFullTextStopListStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001CDCD File Offset: 0x0001AFCD
		public virtual void ExplicitVisit(CreateFullTextStopListStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001CDF3 File Offset: 0x0001AFF3
		public virtual void Visit(AlterFullTextStopListStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001CE04 File Offset: 0x0001B004
		public virtual void ExplicitVisit(AlterFullTextStopListStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001CE2A File Offset: 0x0001B02A
		public virtual void Visit(FullTextStopListAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001CE3B File Offset: 0x0001B03B
		public virtual void ExplicitVisit(FullTextStopListAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001CE5A File Offset: 0x0001B05A
		public virtual void Visit(DropFullTextStopListStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001CE6B File Offset: 0x0001B06B
		public virtual void ExplicitVisit(DropFullTextStopListStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001CE98 File Offset: 0x0001B098
		public virtual void Visit(CreateCryptographicProviderStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001CEA9 File Offset: 0x0001B0A9
		public virtual void ExplicitVisit(CreateCryptographicProviderStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001CECF File Offset: 0x0001B0CF
		public virtual void Visit(AlterCryptographicProviderStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001CEE0 File Offset: 0x0001B0E0
		public virtual void ExplicitVisit(AlterCryptographicProviderStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001CF06 File Offset: 0x0001B106
		public virtual void Visit(DropCryptographicProviderStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001CF17 File Offset: 0x0001B117
		public virtual void ExplicitVisit(DropCryptographicProviderStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001CF44 File Offset: 0x0001B144
		public virtual void Visit(EventSessionObjectName node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001CF55 File Offset: 0x0001B155
		public virtual void ExplicitVisit(EventSessionObjectName node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001CF74 File Offset: 0x0001B174
		public virtual void Visit(EventSessionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001CF85 File Offset: 0x0001B185
		public virtual void ExplicitVisit(EventSessionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001CFAB File Offset: 0x0001B1AB
		public virtual void Visit(CreateEventSessionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001CFBC File Offset: 0x0001B1BC
		public virtual void ExplicitVisit(CreateEventSessionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001CFE9 File Offset: 0x0001B1E9
		public virtual void Visit(EventDeclaration node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001CFFA File Offset: 0x0001B1FA
		public virtual void ExplicitVisit(EventDeclaration node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001D019 File Offset: 0x0001B219
		public virtual void Visit(EventDeclarationSetParameter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001D02A File Offset: 0x0001B22A
		public virtual void ExplicitVisit(EventDeclarationSetParameter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001D049 File Offset: 0x0001B249
		public virtual void Visit(SourceDeclaration node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001D05A File Offset: 0x0001B25A
		public virtual void ExplicitVisit(SourceDeclaration node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001D080 File Offset: 0x0001B280
		public virtual void Visit(EventDeclarationCompareFunctionParameter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001D091 File Offset: 0x0001B291
		public virtual void ExplicitVisit(EventDeclarationCompareFunctionParameter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001D0B7 File Offset: 0x0001B2B7
		public virtual void Visit(TargetDeclaration node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001D0C8 File Offset: 0x0001B2C8
		public virtual void ExplicitVisit(TargetDeclaration node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001D0E7 File Offset: 0x0001B2E7
		public virtual void Visit(SessionOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001D0F8 File Offset: 0x0001B2F8
		public virtual void ExplicitVisit(SessionOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001D117 File Offset: 0x0001B317
		public virtual void Visit(EventRetentionSessionOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001D128 File Offset: 0x0001B328
		public virtual void ExplicitVisit(EventRetentionSessionOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001D14E File Offset: 0x0001B34E
		public virtual void Visit(MemoryPartitionSessionOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001D15F File Offset: 0x0001B35F
		public virtual void ExplicitVisit(MemoryPartitionSessionOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001D185 File Offset: 0x0001B385
		public virtual void Visit(LiteralSessionOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001D196 File Offset: 0x0001B396
		public virtual void ExplicitVisit(LiteralSessionOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001D1BC File Offset: 0x0001B3BC
		public virtual void Visit(MaxDispatchLatencySessionOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001D1CD File Offset: 0x0001B3CD
		public virtual void ExplicitVisit(MaxDispatchLatencySessionOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001D1F3 File Offset: 0x0001B3F3
		public virtual void Visit(OnOffSessionOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001D204 File Offset: 0x0001B404
		public virtual void ExplicitVisit(OnOffSessionOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001D22A File Offset: 0x0001B42A
		public virtual void Visit(AlterEventSessionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001D23B File Offset: 0x0001B43B
		public virtual void ExplicitVisit(AlterEventSessionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001D268 File Offset: 0x0001B468
		public virtual void Visit(DropEventSessionStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001D279 File Offset: 0x0001B479
		public virtual void ExplicitVisit(DropEventSessionStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001D2A6 File Offset: 0x0001B4A6
		public virtual void Visit(AlterResourceGovernorStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001D2B7 File Offset: 0x0001B4B7
		public virtual void ExplicitVisit(AlterResourceGovernorStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001D2DD File Offset: 0x0001B4DD
		public virtual void Visit(CreateSpatialIndexStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001D2EE File Offset: 0x0001B4EE
		public virtual void ExplicitVisit(CreateSpatialIndexStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001D314 File Offset: 0x0001B514
		public virtual void Visit(SpatialIndexOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001D325 File Offset: 0x0001B525
		public virtual void ExplicitVisit(SpatialIndexOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001D344 File Offset: 0x0001B544
		public virtual void Visit(SpatialIndexRegularOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001D355 File Offset: 0x0001B555
		public virtual void ExplicitVisit(SpatialIndexRegularOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001D37B File Offset: 0x0001B57B
		public virtual void Visit(BoundingBoxSpatialIndexOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001D38C File Offset: 0x0001B58C
		public virtual void ExplicitVisit(BoundingBoxSpatialIndexOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001D3B2 File Offset: 0x0001B5B2
		public virtual void Visit(BoundingBoxParameter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001D3C3 File Offset: 0x0001B5C3
		public virtual void ExplicitVisit(BoundingBoxParameter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001D3E2 File Offset: 0x0001B5E2
		public virtual void Visit(GridsSpatialIndexOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001D3F3 File Offset: 0x0001B5F3
		public virtual void ExplicitVisit(GridsSpatialIndexOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001D419 File Offset: 0x0001B619
		public virtual void Visit(GridParameter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001D42A File Offset: 0x0001B62A
		public virtual void ExplicitVisit(GridParameter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001D449 File Offset: 0x0001B649
		public virtual void Visit(CellsPerObjectSpatialIndexOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001D45A File Offset: 0x0001B65A
		public virtual void ExplicitVisit(CellsPerObjectSpatialIndexOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001D480 File Offset: 0x0001B680
		public virtual void Visit(AlterServerConfigurationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001D491 File Offset: 0x0001B691
		public virtual void ExplicitVisit(AlterServerConfigurationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001D4B7 File Offset: 0x0001B6B7
		public virtual void Visit(ProcessAffinityRange node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001D4C8 File Offset: 0x0001B6C8
		public virtual void ExplicitVisit(ProcessAffinityRange node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001D4EE File Offset: 0x0001B6EE
		public virtual void Visit(AvailabilityGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001D4FF File Offset: 0x0001B6FF
		public virtual void ExplicitVisit(AvailabilityGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001D525 File Offset: 0x0001B725
		public virtual void Visit(CreateAvailabilityGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001D536 File Offset: 0x0001B736
		public virtual void ExplicitVisit(CreateAvailabilityGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001D563 File Offset: 0x0001B763
		public virtual void Visit(AlterAvailabilityGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001D574 File Offset: 0x0001B774
		public virtual void ExplicitVisit(AlterAvailabilityGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0001D5A1 File Offset: 0x0001B7A1
		public virtual void Visit(AvailabilityReplica node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0001D5B2 File Offset: 0x0001B7B2
		public virtual void ExplicitVisit(AvailabilityReplica node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0001D5D1 File Offset: 0x0001B7D1
		public virtual void Visit(AvailabilityReplicaOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001D5E2 File Offset: 0x0001B7E2
		public virtual void ExplicitVisit(AvailabilityReplicaOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001D601 File Offset: 0x0001B801
		public virtual void Visit(LiteralReplicaOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001D612 File Offset: 0x0001B812
		public virtual void ExplicitVisit(LiteralReplicaOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001D638 File Offset: 0x0001B838
		public virtual void Visit(AvailabilityModeReplicaOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001D649 File Offset: 0x0001B849
		public virtual void ExplicitVisit(AvailabilityModeReplicaOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001D66F File Offset: 0x0001B86F
		public virtual void Visit(FailoverModeReplicaOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001D680 File Offset: 0x0001B880
		public virtual void ExplicitVisit(FailoverModeReplicaOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001D6A6 File Offset: 0x0001B8A6
		public virtual void Visit(PrimaryRoleReplicaOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001D6B7 File Offset: 0x0001B8B7
		public virtual void ExplicitVisit(PrimaryRoleReplicaOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0001D6DD File Offset: 0x0001B8DD
		public virtual void Visit(SecondaryRoleReplicaOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0001D6EE File Offset: 0x0001B8EE
		public virtual void ExplicitVisit(SecondaryRoleReplicaOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001D714 File Offset: 0x0001B914
		public virtual void Visit(AvailabilityGroupOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001D725 File Offset: 0x0001B925
		public virtual void ExplicitVisit(AvailabilityGroupOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001D744 File Offset: 0x0001B944
		public virtual void Visit(LiteralAvailabilityGroupOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001D755 File Offset: 0x0001B955
		public virtual void ExplicitVisit(LiteralAvailabilityGroupOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001D77B File Offset: 0x0001B97B
		public virtual void Visit(AlterAvailabilityGroupAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001D78C File Offset: 0x0001B98C
		public virtual void ExplicitVisit(AlterAvailabilityGroupAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001D7AB File Offset: 0x0001B9AB
		public virtual void Visit(AlterAvailabilityGroupFailoverAction node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001D7BC File Offset: 0x0001B9BC
		public virtual void ExplicitVisit(AlterAvailabilityGroupFailoverAction node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001D7E2 File Offset: 0x0001B9E2
		public virtual void Visit(AlterAvailabilityGroupFailoverOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001D7F3 File Offset: 0x0001B9F3
		public virtual void ExplicitVisit(AlterAvailabilityGroupFailoverOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001D812 File Offset: 0x0001BA12
		public virtual void Visit(DropAvailabilityGroupStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001D823 File Offset: 0x0001BA23
		public virtual void ExplicitVisit(DropAvailabilityGroupStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001D850 File Offset: 0x0001BA50
		public virtual void Visit(CreateFederationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001D861 File Offset: 0x0001BA61
		public virtual void ExplicitVisit(CreateFederationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001D887 File Offset: 0x0001BA87
		public virtual void Visit(AlterFederationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001D898 File Offset: 0x0001BA98
		public virtual void ExplicitVisit(AlterFederationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001D8BE File Offset: 0x0001BABE
		public virtual void Visit(DropFederationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001D8CF File Offset: 0x0001BACF
		public virtual void ExplicitVisit(DropFederationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001D8FC File Offset: 0x0001BAFC
		public virtual void Visit(UseFederationStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001D90D File Offset: 0x0001BB0D
		public virtual void ExplicitVisit(UseFederationStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001D933 File Offset: 0x0001BB33
		public virtual void Visit(DiskStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001D944 File Offset: 0x0001BB44
		public virtual void ExplicitVisit(DiskStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001D96A File Offset: 0x0001BB6A
		public virtual void Visit(DiskStatementOption node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001D97B File Offset: 0x0001BB7B
		public virtual void ExplicitVisit(DiskStatementOption node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001D99A File Offset: 0x0001BB9A
		public virtual void Visit(CreateColumnStoreIndexStatement node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001D9AB File Offset: 0x0001BBAB
		public virtual void ExplicitVisit(CreateColumnStoreIndexStatement node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001D9D1 File Offset: 0x0001BBD1
		public virtual void Visit(WindowFrameClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0001D9E2 File Offset: 0x0001BBE2
		public virtual void ExplicitVisit(WindowFrameClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001DA01 File Offset: 0x0001BC01
		public virtual void Visit(WindowDelimiter node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001DA12 File Offset: 0x0001BC12
		public virtual void ExplicitVisit(WindowDelimiter node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001DA31 File Offset: 0x0001BC31
		public virtual void Visit(WithinGroupClause node)
		{
			if (!this.VisitBaseType)
			{
				this.Visit(node);
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual void ExplicitVisit(WithinGroupClause node)
		{
			if (this.VisitBaseType)
			{
				this.Visit(node);
			}
			this.Visit(node);
			node.AcceptChildren(this);
		}

		// Token: 0x04000713 RID: 1811
		private readonly bool _visitBaseType;
	}
}
