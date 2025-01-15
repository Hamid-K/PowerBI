using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020000E2 RID: 226
	public class CSharpMigrationCodeGenerator : MigrationCodeGenerator
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x00027DE0 File Offset: 0x00025FE0
		public override ScaffoldedMigration Generate(string migrationId, IEnumerable<MigrationOperation> operations, string sourceModel, string targetModel, string @namespace, string className)
		{
			Check.NotEmpty(migrationId, "migrationId");
			Check.NotNull<IEnumerable<MigrationOperation>>(operations, "operations");
			Check.NotEmpty(targetModel, "targetModel");
			Check.NotEmpty(className, "className");
			className = this.ScrubName(className);
			this._newTableForeignKeys = (from ct in operations.OfType<CreateTableOperation>()
				from cfk in operations.OfType<AddForeignKeyOperation>()
				where ct.Name.EqualsIgnoreCase(cfk.DependentTable)
				select Tuple.Create<CreateTableOperation, AddForeignKeyOperation>(ct, cfk)).ToList<Tuple<CreateTableOperation, AddForeignKeyOperation>>();
			this._newTableIndexes = (from ct in operations.OfType<CreateTableOperation>()
				from ci in operations.OfType<CreateIndexOperation>()
				where ct.Name.EqualsIgnoreCase(ci.Table)
				select Tuple.Create<CreateTableOperation, CreateIndexOperation>(ct, ci)).ToList<Tuple<CreateTableOperation, CreateIndexOperation>>();
			ScaffoldedMigration scaffoldedMigration = new ScaffoldedMigration
			{
				MigrationId = migrationId,
				Language = "cs",
				UserCode = this.Generate(operations, @namespace, className),
				DesignerCode = this.Generate(migrationId, sourceModel, targetModel, @namespace, className)
			};
			if (!string.IsNullOrWhiteSpace(sourceModel))
			{
				scaffoldedMigration.Resources.Add("Source", sourceModel);
			}
			scaffoldedMigration.Resources.Add("Target", targetModel);
			return scaffoldedMigration;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00027FC8 File Offset: 0x000261C8
		protected virtual string Generate(IEnumerable<MigrationOperation> operations, string @namespace, string className)
		{
			Check.NotNull<IEnumerable<MigrationOperation>>(operations, "operations");
			Check.NotEmpty(className, "className");
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (IndentedTextWriter writer = new IndentedTextWriter(stringWriter))
				{
					this.WriteClassStart(@namespace, className, writer, "DbMigration", false, this.GetNamespaces(operations));
					writer.WriteLine("public override void Up()");
					writer.WriteLine("{");
					IndentedTextWriter writer5 = writer;
					int num = writer5.Indent;
					writer5.Indent = num + 1;
					operations.Except(this._newTableForeignKeys.Select((Tuple<CreateTableOperation, AddForeignKeyOperation> t) => t.Item2)).Except(this._newTableIndexes.Select((Tuple<CreateTableOperation, CreateIndexOperation> t) => t.Item2)).Each(delegate(dynamic o)
					{
						if (CSharpMigrationCodeGenerator.<>o__3.<>p__0 == null)
						{
							CSharpMigrationCodeGenerator.<>o__3.<>p__0 = CallSite<Action<CallSite, CSharpMigrationCodeGenerator, object, IndentedTextWriter>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "Generate", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						CSharpMigrationCodeGenerator.<>o__3.<>p__0.Target(CSharpMigrationCodeGenerator.<>o__3.<>p__0, this, o, writer);
					});
					IndentedTextWriter writer2 = writer;
					num = writer2.Indent;
					writer2.Indent = num - 1;
					writer.WriteLine("}");
					writer.WriteLine();
					writer.WriteLine("public override void Down()");
					writer.WriteLine("{");
					IndentedTextWriter writer3 = writer;
					num = writer3.Indent;
					writer3.Indent = num + 1;
					operations = (from o in operations
						select o.Inverse into o
						where o != null
						select o).Reverse<MigrationOperation>();
					bool flag = operations.Any((MigrationOperation o) => o is NotSupportedOperation);
					operations.Where((MigrationOperation o) => !(o is NotSupportedOperation)).Each(delegate(dynamic o)
					{
						if (CSharpMigrationCodeGenerator.<>o__3.<>p__1 == null)
						{
							CSharpMigrationCodeGenerator.<>o__3.<>p__1 = CallSite<Action<CallSite, CSharpMigrationCodeGenerator, object, IndentedTextWriter>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "Generate", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						CSharpMigrationCodeGenerator.<>o__3.<>p__1.Target(CSharpMigrationCodeGenerator.<>o__3.<>p__1, this, o, writer);
					});
					if (flag)
					{
						writer.Write("throw new NotSupportedException(");
						writer.Write(this.Generate(Strings.ScaffoldSprocInDownNotSupported));
						writer.WriteLine(");");
					}
					IndentedTextWriter writer4 = writer;
					num = writer4.Indent;
					writer4.Indent = num - 1;
					writer.WriteLine("}");
					this.WriteClassEnd(@namespace, writer);
				}
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000282AC File Offset: 0x000264AC
		protected virtual string Generate(string migrationId, string sourceModel, string targetModel, string @namespace, string className)
		{
			Check.NotEmpty(migrationId, "migrationId");
			Check.NotEmpty(targetModel, "targetModel");
			Check.NotEmpty(className, "className");
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (IndentedTextWriter indentedTextWriter = new IndentedTextWriter(stringWriter))
				{
					indentedTextWriter.WriteLine("// <auto-generated />");
					this.WriteClassStart(@namespace, className, indentedTextWriter, "IMigrationMetadata", true, null);
					indentedTextWriter.Write("private readonly ResourceManager Resources = new ResourceManager(typeof(");
					indentedTextWriter.Write(className);
					indentedTextWriter.WriteLine("));");
					indentedTextWriter.WriteLine();
					this.WriteProperty("Id", this.Quote(migrationId), indentedTextWriter);
					indentedTextWriter.WriteLine();
					this.WriteProperty("Source", (sourceModel == null) ? null : "Resources.GetString(\"Source\")", indentedTextWriter);
					indentedTextWriter.WriteLine();
					this.WriteProperty("Target", "Resources.GetString(\"Target\")", indentedTextWriter);
					this.WriteClassEnd(@namespace, indentedTextWriter);
				}
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000283C0 File Offset: 0x000265C0
		protected virtual void WriteProperty(string name, string value, IndentedTextWriter writer)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("string IMigrationMetadata.");
			writer.WriteLine(name);
			writer.WriteLine("{");
			int num = writer.Indent;
			writer.Indent = num + 1;
			writer.Write("get { return ");
			writer.Write(value ?? "null");
			writer.WriteLine("; }");
			num = writer.Indent;
			writer.Indent = num - 1;
			writer.WriteLine("}");
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00028453 File Offset: 0x00026653
		protected virtual void WriteClassAttributes(IndentedTextWriter writer, bool designer)
		{
			if (designer)
			{
				writer.WriteLine("[GeneratedCode(\"EntityFramework.Migrations\", \"{0}\")]", typeof(CSharpMigrationCodeGenerator).Assembly().GetInformationalVersion());
			}
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00028478 File Offset: 0x00026678
		protected virtual void WriteClassStart(string @namespace, string className, IndentedTextWriter writer, string @base, bool designer = false, IEnumerable<string> namespaces = null)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			Check.NotEmpty(className, "className");
			Check.NotEmpty(@base, "base");
			int num;
			if (!string.IsNullOrWhiteSpace(@namespace))
			{
				writer.Write("namespace ");
				writer.WriteLine(@namespace);
				writer.WriteLine("{");
				IndentedTextWriter writer2 = writer;
				num = writer2.Indent;
				writer2.Indent = num + 1;
			}
			(namespaces ?? this.GetDefaultNamespaces(designer)).Each(delegate(string n)
			{
				writer.WriteLine("using " + n + ";");
			});
			writer.WriteLine();
			this.WriteClassAttributes(writer, designer);
			writer.Write("public ");
			if (designer)
			{
				writer.Write("sealed ");
			}
			writer.Write("partial class ");
			writer.Write(className);
			writer.Write(" : ");
			writer.Write(@base);
			writer.WriteLine();
			writer.WriteLine("{");
			IndentedTextWriter writer3 = writer;
			num = writer3.Indent;
			writer3.Indent = num + 1;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000285CC File Offset: 0x000267CC
		protected virtual void WriteClassEnd(string @namespace, IndentedTextWriter writer)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			int num = writer.Indent;
			writer.Indent = num - 1;
			writer.WriteLine("}");
			if (!string.IsNullOrWhiteSpace(@namespace))
			{
				num = writer.Indent;
				writer.Indent = num - 1;
				writer.WriteLine("}");
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00028624 File Offset: 0x00026824
		protected virtual void Generate(AddColumnOperation addColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddColumnOperation>(addColumnOperation, "addColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AddColumn(");
			writer.Write(this.Quote(addColumnOperation.Table));
			writer.Write(", ");
			writer.Write(this.Quote(addColumnOperation.Column.Name));
			writer.Write(", c =>");
			this.Generate(addColumnOperation.Column, writer, false);
			writer.WriteLine(");");
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000286AC File Offset: 0x000268AC
		protected virtual void Generate(DropColumnOperation dropColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropColumnOperation>(dropColumnOperation, "dropColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropColumn(");
			writer.Write(this.Quote(dropColumnOperation.Table));
			writer.Write(", ");
			writer.Write(this.Quote(dropColumnOperation.Name));
			if (dropColumnOperation.RemovedAnnotations.Any<KeyValuePair<string, object>>())
			{
				int num = writer.Indent;
				writer.Indent = num + 1;
				writer.WriteLine(",");
				writer.Write("removedAnnotations: ");
				this.GenerateAnnotations(dropColumnOperation.RemovedAnnotations, writer);
				num = writer.Indent;
				writer.Indent = num - 1;
			}
			writer.WriteLine(");");
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00028768 File Offset: 0x00026968
		protected virtual void Generate(AlterColumnOperation alterColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AlterColumnOperation>(alterColumnOperation, "alterColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AlterColumn(");
			writer.Write(this.Quote(alterColumnOperation.Table));
			writer.Write(", ");
			writer.Write(this.Quote(alterColumnOperation.Column.Name));
			writer.Write(", c =>");
			this.Generate(alterColumnOperation.Column, writer, false);
			writer.WriteLine(");");
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x000287F0 File Offset: 0x000269F0
		protected internal virtual void GenerateAnnotations(IDictionary<string, object> annotations, IndentedTextWriter writer)
		{
			Check.NotNull<IDictionary<string, object>>(annotations, "annotations");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("new Dictionary<string, object>");
			writer.WriteLine("{");
			int num = writer.Indent;
			writer.Indent = num + 1;
			foreach (string text in annotations.Keys.OrderBy((string k) => k))
			{
				writer.Write("{ ");
				writer.Write(this.Quote(text) + ", ");
				this.GenerateAnnotation(text, annotations[text], writer);
				writer.WriteLine(" },");
			}
			num = writer.Indent;
			writer.Indent = num - 1;
			writer.Write("}");
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x000288F0 File Offset: 0x00026AF0
		protected internal virtual void GenerateAnnotations(IDictionary<string, AnnotationValues> annotations, IndentedTextWriter writer)
		{
			Check.NotNull<IDictionary<string, AnnotationValues>>(annotations, "annotations");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("new Dictionary<string, AnnotationValues>");
			writer.WriteLine("{");
			int num = writer.Indent;
			writer.Indent = num + 1;
			if (annotations != null)
			{
				foreach (string text in annotations.Keys.OrderBy((string k) => k))
				{
					writer.WriteLine("{ ");
					num = writer.Indent;
					writer.Indent = num + 1;
					writer.WriteLine(this.Quote(text) + ",");
					writer.Write("new AnnotationValues(oldValue: ");
					this.GenerateAnnotation(text, annotations[text].OldValue, writer);
					writer.Write(", newValue: ");
					this.GenerateAnnotation(text, annotations[text].NewValue, writer);
					writer.WriteLine(")");
					num = writer.Indent;
					writer.Indent = num - 1;
					writer.WriteLine("},");
				}
			}
			num = writer.Indent;
			writer.Indent = num - 1;
			writer.Write("}");
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00028A58 File Offset: 0x00026C58
		protected internal virtual void GenerateAnnotation(string name, object annotation, IndentedTextWriter writer)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			if (annotation == null)
			{
				writer.Write("null");
				return;
			}
			Func<AnnotationCodeGenerator> func;
			if (this.AnnotationGenerators.TryGetValue(name, out func) && func != null)
			{
				func().Generate(name, annotation, writer);
				return;
			}
			writer.Write(this.Quote(annotation.ToString()));
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00028AC0 File Offset: 0x00026CC0
		protected virtual void Generate(CreateProcedureOperation createProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateProcedureOperation>(createProcedureOperation, "createProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			this.Generate(createProcedureOperation, "CreateStoredProcedure", writer);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00028AE7 File Offset: 0x00026CE7
		protected virtual void Generate(AlterProcedureOperation alterProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AlterProcedureOperation>(alterProcedureOperation, "alterProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			this.Generate(alterProcedureOperation, "AlterStoredProcedure", writer);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00028B10 File Offset: 0x00026D10
		private void Generate(ProcedureOperation procedureOperation, string methodName, IndentedTextWriter writer)
		{
			writer.Write(methodName);
			writer.WriteLine("(");
			IndentedTextWriter writer2 = writer;
			int num = writer2.Indent;
			writer2.Indent = num + 1;
			writer.Write(this.Quote(procedureOperation.Name));
			writer.WriteLine(",");
			if (procedureOperation.Parameters.Any<ParameterModel>())
			{
				writer.WriteLine("p => new");
				IndentedTextWriter writer3 = writer;
				num = writer3.Indent;
				writer3.Indent = num + 1;
				writer.WriteLine("{");
				IndentedTextWriter writer4 = writer;
				num = writer4.Indent;
				writer4.Indent = num + 1;
				procedureOperation.Parameters.Each(delegate(ParameterModel p)
				{
					string text2 = this.ScrubName(p.Name);
					writer.Write(text2);
					writer.Write(" =");
					this.Generate(p, writer, !string.Equals(p.Name, text2, StringComparison.Ordinal));
					writer.WriteLine(",");
				});
				IndentedTextWriter writer5 = writer;
				num = writer5.Indent;
				writer5.Indent = num - 1;
				writer.WriteLine("},");
				IndentedTextWriter writer6 = writer;
				num = writer6.Indent;
				writer6.Indent = num - 1;
			}
			writer.Write("body:");
			if (!string.IsNullOrWhiteSpace(procedureOperation.BodySql))
			{
				writer.WriteLine();
				IndentedTextWriter writer7 = writer;
				num = writer7.Indent;
				writer7.Indent = num + 1;
				string text = writer.NewLine + writer.CurrentIndentation() + "  ";
				writer.Write("@");
				writer.WriteLine(this.Generate(procedureOperation.BodySql.Replace(Environment.NewLine, text)));
				IndentedTextWriter writer8 = writer;
				num = writer8.Indent;
				writer8.Indent = num - 1;
			}
			else
			{
				writer.WriteLine(" \"\"");
			}
			IndentedTextWriter writer9 = writer;
			num = writer9.Indent;
			writer9.Indent = num - 1;
			writer.WriteLine(");");
			writer.WriteLine();
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00028D20 File Offset: 0x00026F20
		protected virtual void Generate(ParameterModel parameterModel, IndentedTextWriter writer, bool emitName = false)
		{
			Check.NotNull<ParameterModel>(parameterModel, "parameterModel");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write(" p.");
			writer.Write(this.TranslateColumnType(parameterModel.Type));
			writer.Write("(");
			List<string> list = new List<string>();
			if (emitName)
			{
				list.Add("name: " + this.Quote(parameterModel.Name));
			}
			if (parameterModel.MaxLength != null)
			{
				list.Add("maxLength: " + parameterModel.MaxLength.ToString());
			}
			if (parameterModel.Precision != null)
			{
				list.Add("precision: " + parameterModel.Precision.ToString());
			}
			if (parameterModel.Scale != null)
			{
				list.Add("scale: " + parameterModel.Scale.ToString());
			}
			if (parameterModel.IsFixedLength != null)
			{
				list.Add("fixedLength: " + parameterModel.IsFixedLength.ToString().ToLowerInvariant());
			}
			if (parameterModel.IsUnicode != null)
			{
				list.Add("unicode: " + parameterModel.IsUnicode.ToString().ToLowerInvariant());
			}
			if (parameterModel.DefaultValue != null)
			{
				if (CSharpMigrationCodeGenerator.<>o__18.<>p__2 == null)
				{
					CSharpMigrationCodeGenerator.<>o__18.<>p__2 = CallSite<Action<CallSite, List<string>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, List<string>, object> target = CSharpMigrationCodeGenerator.<>o__18.<>p__2.Target;
				CallSite <>p__ = CSharpMigrationCodeGenerator.<>o__18.<>p__2;
				List<string> list2 = list;
				if (CSharpMigrationCodeGenerator.<>o__18.<>p__1 == null)
				{
					CSharpMigrationCodeGenerator.<>o__18.<>p__1 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, string, object, object> target2 = CSharpMigrationCodeGenerator.<>o__18.<>p__1.Target;
				CallSite <>p__2 = CSharpMigrationCodeGenerator.<>o__18.<>p__1;
				string text = "defaultValue: ";
				if (CSharpMigrationCodeGenerator.<>o__18.<>p__0 == null)
				{
					CSharpMigrationCodeGenerator.<>o__18.<>p__0 = CallSite<Func<CallSite, CSharpMigrationCodeGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__, list2, target2(<>p__2, text, CSharpMigrationCodeGenerator.<>o__18.<>p__0.Target(CSharpMigrationCodeGenerator.<>o__18.<>p__0, this, parameterModel.DefaultValue)));
			}
			if (!string.IsNullOrWhiteSpace(parameterModel.DefaultValueSql))
			{
				list.Add("defaultValueSql: " + this.Quote(parameterModel.DefaultValueSql));
			}
			if (!string.IsNullOrWhiteSpace(parameterModel.StoreType))
			{
				list.Add("storeType: " + this.Quote(parameterModel.StoreType));
			}
			if (parameterModel.IsOutParameter)
			{
				list.Add("outParameter: true");
			}
			writer.Write(list.Join(null, ", "));
			writer.Write(")");
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00029038 File Offset: 0x00027238
		protected virtual void Generate(DropProcedureOperation dropProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropProcedureOperation>(dropProcedureOperation, "dropProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropStoredProcedure(");
			writer.Write(this.Quote(dropProcedureOperation.Name));
			writer.WriteLine(");");
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00029088 File Offset: 0x00027288
		protected virtual void Generate(CreateTableOperation createTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("CreateTable(");
			IndentedTextWriter writer2 = writer;
			int num = writer2.Indent;
			writer2.Indent = num + 1;
			writer.Write(this.Quote(createTableOperation.Name));
			writer.WriteLine(",");
			writer.WriteLine("c => new");
			IndentedTextWriter writer3 = writer;
			num = writer3.Indent;
			writer3.Indent = num + 1;
			writer.WriteLine("{");
			IndentedTextWriter writer4 = writer;
			num = writer4.Indent;
			writer4.Indent = num + 1;
			createTableOperation.Columns.Each(delegate(ColumnModel c)
			{
				string text = this.ScrubName(c.Name);
				writer.Write(text);
				writer.Write(" =");
				this.Generate(c, writer, !string.Equals(c.Name, text, StringComparison.Ordinal));
				writer.WriteLine(",");
			});
			IndentedTextWriter writer5 = writer;
			num = writer5.Indent;
			writer5.Indent = num - 1;
			writer.Write("}");
			IndentedTextWriter writer6 = writer;
			num = writer6.Indent;
			writer6.Indent = num - 1;
			if (createTableOperation.Annotations.Any<KeyValuePair<string, object>>())
			{
				writer.WriteLine(",");
				writer.Write("annotations: ");
				this.GenerateAnnotations(createTableOperation.Annotations, writer);
			}
			writer.Write(")");
			this.GenerateInline(createTableOperation.PrimaryKey, writer);
			this._newTableForeignKeys.Where((Tuple<CreateTableOperation, AddForeignKeyOperation> t) => t.Item1 == createTableOperation).Each(delegate(Tuple<CreateTableOperation, AddForeignKeyOperation> t)
			{
				this.GenerateInline(t.Item2, writer);
			});
			this._newTableIndexes.Where((Tuple<CreateTableOperation, CreateIndexOperation> t) => t.Item1 == createTableOperation).Each(delegate(Tuple<CreateTableOperation, CreateIndexOperation> t)
			{
				this.GenerateInline(t.Item2, writer);
			});
			writer.WriteLine(";");
			IndentedTextWriter writer7 = writer;
			num = writer7.Indent;
			writer7.Indent = num - 1;
			writer.WriteLine();
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000292B4 File Offset: 0x000274B4
		protected internal virtual void Generate(AlterTableOperation alterTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AlterTableOperation>(alterTableOperation, "alterTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("AlterTableAnnotations(");
			IndentedTextWriter writer2 = writer;
			int num = writer2.Indent;
			writer2.Indent = num + 1;
			writer.Write(this.Quote(alterTableOperation.Name));
			writer.WriteLine(",");
			writer.WriteLine("c => new");
			IndentedTextWriter writer3 = writer;
			num = writer3.Indent;
			writer3.Indent = num + 1;
			writer.WriteLine("{");
			IndentedTextWriter writer4 = writer;
			num = writer4.Indent;
			writer4.Indent = num + 1;
			alterTableOperation.Columns.Each(delegate(ColumnModel c)
			{
				string text = this.ScrubName(c.Name);
				writer.Write(text);
				writer.Write(" =");
				this.Generate(c, writer, !string.Equals(c.Name, text, StringComparison.Ordinal));
				writer.WriteLine(",");
			});
			IndentedTextWriter writer5 = writer;
			num = writer5.Indent;
			writer5.Indent = num - 1;
			writer.Write("}");
			IndentedTextWriter writer6 = writer;
			num = writer6.Indent;
			writer6.Indent = num - 1;
			if (alterTableOperation.Annotations.Any<KeyValuePair<string, AnnotationValues>>())
			{
				writer.WriteLine(",");
				writer.Write("annotations: ");
				this.GenerateAnnotations(alterTableOperation.Annotations, writer);
			}
			writer.Write(")");
			writer.WriteLine(";");
			IndentedTextWriter writer7 = writer;
			num = writer7.Indent;
			writer7.Indent = num - 1;
			writer.WriteLine();
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00029458 File Offset: 0x00027658
		protected virtual void GenerateInline(AddPrimaryKeyOperation addPrimaryKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			if (addPrimaryKeyOperation != null)
			{
				writer.WriteLine();
				writer.Write(".PrimaryKey(");
				this.Generate(addPrimaryKeyOperation.Columns, writer);
				if (!addPrimaryKeyOperation.HasDefaultName)
				{
					writer.Write(", name: ");
					writer.Write(this.Quote(addPrimaryKeyOperation.Name));
				}
				if (!addPrimaryKeyOperation.IsClustered)
				{
					writer.Write(", clustered: false");
				}
				writer.Write(")");
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000294D8 File Offset: 0x000276D8
		protected virtual void GenerateInline(AddForeignKeyOperation addForeignKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddForeignKeyOperation>(addForeignKeyOperation, "addForeignKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine();
			writer.Write(".ForeignKey(" + this.Quote(addForeignKeyOperation.PrincipalTable) + ", ");
			this.Generate(addForeignKeyOperation.DependentColumns, writer);
			if (addForeignKeyOperation.CascadeDelete)
			{
				writer.Write(", cascadeDelete: true");
			}
			writer.Write(")");
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00029550 File Offset: 0x00027750
		protected virtual void GenerateInline(CreateIndexOperation createIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateIndexOperation>(createIndexOperation, "createIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine();
			writer.Write(".Index(");
			this.Generate(createIndexOperation.Columns, writer);
			this.WriteIndexParameters(createIndexOperation, writer);
			writer.Write(")");
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000295A8 File Offset: 0x000277A8
		protected virtual void Generate(IEnumerable<string> columns, IndentedTextWriter writer)
		{
			Check.NotNull<IEnumerable<string>>(columns, "columns");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("t => ");
			if (columns.Count<string>() == 1)
			{
				writer.Write("t." + this.ScrubName(columns.Single<string>()));
				return;
			}
			writer.Write("new { " + columns.Join((string c) => "t." + this.ScrubName(c), ", ") + " }");
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0002962C File Offset: 0x0002782C
		protected virtual void Generate(AddPrimaryKeyOperation addPrimaryKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddPrimaryKeyOperation>(addPrimaryKeyOperation, "addPrimaryKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AddPrimaryKey(");
			writer.Write(this.Quote(addPrimaryKeyOperation.Table));
			writer.Write(", ");
			bool flag = addPrimaryKeyOperation.Columns.Count<string>() > 1;
			if (flag)
			{
				writer.Write("new[] { ");
			}
			writer.Write(addPrimaryKeyOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
			if (flag)
			{
				writer.Write(" }");
			}
			if (!addPrimaryKeyOperation.HasDefaultName)
			{
				writer.Write(", name: ");
				writer.Write(this.Quote(addPrimaryKeyOperation.Name));
			}
			if (!addPrimaryKeyOperation.IsClustered)
			{
				writer.Write(", clustered: false");
			}
			writer.WriteLine(");");
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00029708 File Offset: 0x00027908
		protected virtual void Generate(DropPrimaryKeyOperation dropPrimaryKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropPrimaryKeyOperation>(dropPrimaryKeyOperation, "dropPrimaryKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropPrimaryKey(");
			writer.Write(this.Quote(dropPrimaryKeyOperation.Table));
			if (!dropPrimaryKeyOperation.HasDefaultName)
			{
				writer.Write(", name: ");
				writer.Write(this.Quote(dropPrimaryKeyOperation.Name));
			}
			writer.WriteLine(");");
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0002977C File Offset: 0x0002797C
		protected virtual void Generate(AddForeignKeyOperation addForeignKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddForeignKeyOperation>(addForeignKeyOperation, "addForeignKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AddForeignKey(");
			writer.Write(this.Quote(addForeignKeyOperation.DependentTable));
			writer.Write(", ");
			bool flag = addForeignKeyOperation.DependentColumns.Count<string>() > 1;
			if (flag)
			{
				writer.Write("new[] { ");
			}
			writer.Write(addForeignKeyOperation.DependentColumns.Join(new Func<string, string>(this.Quote), ", "));
			if (flag)
			{
				writer.Write(" }");
			}
			writer.Write(", ");
			writer.Write(this.Quote(addForeignKeyOperation.PrincipalTable));
			if (addForeignKeyOperation.PrincipalColumns.Any<string>())
			{
				writer.Write(", ");
				if (flag)
				{
					writer.Write("new[] { ");
				}
				writer.Write(addForeignKeyOperation.PrincipalColumns.Join(new Func<string, string>(this.Quote), ", "));
				if (flag)
				{
					writer.Write(" }");
				}
			}
			if (addForeignKeyOperation.CascadeDelete)
			{
				writer.Write(", cascadeDelete: true");
			}
			if (!addForeignKeyOperation.HasDefaultName)
			{
				writer.Write(", name: ");
				writer.Write(this.Quote(addForeignKeyOperation.Name));
			}
			writer.WriteLine(");");
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000298D0 File Offset: 0x00027AD0
		protected virtual void Generate(DropForeignKeyOperation dropForeignKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropForeignKeyOperation>(dropForeignKeyOperation, "dropForeignKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropForeignKey(");
			writer.Write(this.Quote(dropForeignKeyOperation.DependentTable));
			writer.Write(", ");
			if (!dropForeignKeyOperation.HasDefaultName)
			{
				writer.Write(this.Quote(dropForeignKeyOperation.Name));
			}
			else
			{
				bool flag = dropForeignKeyOperation.DependentColumns.Count<string>() > 1;
				if (flag)
				{
					writer.Write("new[] { ");
				}
				writer.Write(dropForeignKeyOperation.DependentColumns.Join(new Func<string, string>(this.Quote), ", "));
				if (flag)
				{
					writer.Write(" }");
				}
				writer.Write(", ");
				writer.Write(this.Quote(dropForeignKeyOperation.PrincipalTable));
			}
			writer.WriteLine(");");
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000299B0 File Offset: 0x00027BB0
		protected virtual void Generate(CreateIndexOperation createIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateIndexOperation>(createIndexOperation, "createIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("CreateIndex(");
			writer.Write(this.Quote(createIndexOperation.Table));
			writer.Write(", ");
			bool flag = createIndexOperation.Columns.Count<string>() > 1;
			if (flag)
			{
				writer.Write("new[] { ");
			}
			writer.Write(createIndexOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
			if (flag)
			{
				writer.Write(" }");
			}
			this.WriteIndexParameters(createIndexOperation, writer);
			writer.WriteLine(");");
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00029A5C File Offset: 0x00027C5C
		private void WriteIndexParameters(CreateIndexOperation createIndexOperation, IndentedTextWriter writer)
		{
			if (createIndexOperation.IsUnique)
			{
				writer.Write(", unique: true");
			}
			if (createIndexOperation.IsClustered)
			{
				writer.Write(", clustered: true");
			}
			if (!createIndexOperation.HasDefaultName)
			{
				writer.Write(", name: ");
				writer.Write(this.Quote(createIndexOperation.Name));
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00029AB4 File Offset: 0x00027CB4
		protected virtual void Generate(DropIndexOperation dropIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropIndexOperation>(dropIndexOperation, "dropIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropIndex(");
			writer.Write(this.Quote(dropIndexOperation.Table));
			writer.Write(", ");
			if (!dropIndexOperation.HasDefaultName)
			{
				writer.Write(this.Quote(dropIndexOperation.Name));
			}
			else
			{
				writer.Write("new[] { ");
				writer.Write(dropIndexOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
				writer.Write(" }");
			}
			writer.WriteLine(");");
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00029B64 File Offset: 0x00027D64
		protected virtual void Generate(ColumnModel column, IndentedTextWriter writer, bool emitName = false)
		{
			Check.NotNull<ColumnModel>(column, "column");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write(" c.");
			writer.Write(this.TranslateColumnType(column.Type));
			writer.Write("(");
			List<string> list = new List<string>();
			if (emitName)
			{
				list.Add("name: " + this.Quote(column.Name));
			}
			bool? isNullable = column.IsNullable;
			bool flag = false;
			if ((isNullable.GetValueOrDefault() == flag) & (isNullable != null))
			{
				list.Add("nullable: false");
			}
			if (column.MaxLength != null)
			{
				list.Add("maxLength: " + column.MaxLength.ToString());
			}
			if (column.Precision != null)
			{
				list.Add("precision: " + column.Precision.ToString());
			}
			if (column.Scale != null)
			{
				list.Add("scale: " + column.Scale.ToString());
			}
			if (column.IsFixedLength != null)
			{
				list.Add("fixedLength: " + column.IsFixedLength.ToString().ToLowerInvariant());
			}
			if (column.IsUnicode != null)
			{
				list.Add("unicode: " + column.IsUnicode.ToString().ToLowerInvariant());
			}
			if (column.IsIdentity)
			{
				list.Add("identity: true");
			}
			if (column.DefaultValue != null)
			{
				if (CSharpMigrationCodeGenerator.<>o__33.<>p__2 == null)
				{
					CSharpMigrationCodeGenerator.<>o__33.<>p__2 = CallSite<Action<CallSite, List<string>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, List<string>, object> target = CSharpMigrationCodeGenerator.<>o__33.<>p__2.Target;
				CallSite <>p__ = CSharpMigrationCodeGenerator.<>o__33.<>p__2;
				List<string> list2 = list;
				if (CSharpMigrationCodeGenerator.<>o__33.<>p__1 == null)
				{
					CSharpMigrationCodeGenerator.<>o__33.<>p__1 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, string, object, object> target2 = CSharpMigrationCodeGenerator.<>o__33.<>p__1.Target;
				CallSite <>p__2 = CSharpMigrationCodeGenerator.<>o__33.<>p__1;
				string text = "defaultValue: ";
				if (CSharpMigrationCodeGenerator.<>o__33.<>p__0 == null)
				{
					CSharpMigrationCodeGenerator.<>o__33.<>p__0 = CallSite<Func<CallSite, CSharpMigrationCodeGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__, list2, target2(<>p__2, text, CSharpMigrationCodeGenerator.<>o__33.<>p__0.Target(CSharpMigrationCodeGenerator.<>o__33.<>p__0, this, column.DefaultValue)));
			}
			if (!string.IsNullOrWhiteSpace(column.DefaultValueSql))
			{
				list.Add("defaultValueSql: " + this.Quote(column.DefaultValueSql));
			}
			if (column.IsTimestamp)
			{
				list.Add("timestamp: true");
			}
			if (!string.IsNullOrWhiteSpace(column.StoreType))
			{
				list.Add("storeType: " + this.Quote(column.StoreType));
			}
			writer.Write(list.Join(null, ", "));
			if (column.Annotations.Any<KeyValuePair<string, AnnotationValues>>())
			{
				int num = writer.Indent;
				writer.Indent = num + 1;
				writer.WriteLine(list.Any<string>() ? "," : "");
				writer.Write("annotations: ");
				this.GenerateAnnotations(column.Annotations, writer);
				num = writer.Indent;
				writer.Indent = num - 1;
			}
			writer.Write(")");
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00029F1E File Offset: 0x0002811E
		protected virtual string Generate(byte[] defaultValue)
		{
			return "new byte[] {" + defaultValue.Join(null, ", ") + "}";
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00029F3C File Offset: 0x0002813C
		protected virtual string Generate(DateTime defaultValue)
		{
			return string.Concat(new string[]
			{
				"new DateTime(",
				defaultValue.Ticks.ToString(),
				", DateTimeKind.",
				Enum.GetName(typeof(DateTimeKind), defaultValue.Kind),
				")"
			});
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00029F9C File Offset: 0x0002819C
		protected virtual string Generate(DateTimeOffset defaultValue)
		{
			return string.Concat(new string[]
			{
				"new DateTimeOffset(",
				defaultValue.Ticks.ToString(),
				", new TimeSpan(",
				defaultValue.Offset.Ticks.ToString(),
				"))"
			});
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00029FF8 File Offset: 0x000281F8
		protected virtual string Generate(decimal defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture) + "m";
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0002A010 File Offset: 0x00028210
		protected virtual string Generate(Guid defaultValue)
		{
			string text = "new Guid(\"";
			Guid guid = defaultValue;
			return text + guid.ToString() + "\")";
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0002A03B File Offset: 0x0002823B
		protected virtual string Generate(long defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0002A049 File Offset: 0x00028249
		protected virtual string Generate(float defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture) + "f";
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0002A061 File Offset: 0x00028261
		protected virtual string Generate(string defaultValue)
		{
			return this.Quote(defaultValue);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0002A06C File Offset: 0x0002826C
		protected virtual string Generate(TimeSpan defaultValue)
		{
			return "new TimeSpan(" + defaultValue.Ticks.ToString() + ")";
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0002A097 File Offset: 0x00028297
		protected virtual string Generate(HierarchyId defaultValue)
		{
			return "new HierarchyId(\"" + ((defaultValue != null) ? defaultValue.ToString() : null) + "\")";
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0002A0B8 File Offset: 0x000282B8
		protected virtual string Generate(DbGeography defaultValue)
		{
			return string.Concat(new string[]
			{
				"DbGeography.FromText(\"",
				defaultValue.AsText(),
				"\", ",
				defaultValue.CoordinateSystemId.ToString(),
				")"
			});
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0002A104 File Offset: 0x00028304
		protected virtual string Generate(DbGeometry defaultValue)
		{
			return string.Concat(new string[]
			{
				"DbGeometry.FromText(\"",
				defaultValue.AsText(),
				"\", ",
				defaultValue.CoordinateSystemId.ToString(),
				")"
			});
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0002A14E File Offset: 0x0002834E
		protected virtual string Generate(object defaultValue)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { defaultValue }).ToLowerInvariant();
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0002A170 File Offset: 0x00028370
		protected virtual void Generate(DropTableOperation dropTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropTableOperation>(dropTableOperation, "dropTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropTable(");
			writer.Write(this.Quote(dropTableOperation.Name));
			if (dropTableOperation.RemovedAnnotations.Any<KeyValuePair<string, object>>())
			{
				int num = writer.Indent;
				writer.Indent = num + 1;
				writer.WriteLine(",");
				writer.Write("removedAnnotations: ");
				this.GenerateAnnotations(dropTableOperation.RemovedAnnotations, writer);
				num = writer.Indent;
				writer.Indent = num - 1;
			}
			IDictionary<string, IDictionary<string, object>> removedColumnAnnotations = dropTableOperation.RemovedColumnAnnotations;
			if (removedColumnAnnotations.Any<KeyValuePair<string, IDictionary<string, object>>>())
			{
				int num = writer.Indent;
				writer.Indent = num + 1;
				writer.WriteLine(",");
				writer.Write("removedColumnAnnotations: ");
				writer.WriteLine("new Dictionary<string, IDictionary<string, object>>");
				writer.WriteLine("{");
				num = writer.Indent;
				writer.Indent = num + 1;
				foreach (string text in removedColumnAnnotations.Keys.OrderBy((string k) => k))
				{
					writer.WriteLine("{");
					num = writer.Indent;
					writer.Indent = num + 1;
					writer.WriteLine(this.Quote(text) + ",");
					this.GenerateAnnotations(removedColumnAnnotations[text], writer);
					writer.WriteLine();
					num = writer.Indent;
					writer.Indent = num - 1;
					writer.WriteLine("},");
				}
				num = writer.Indent;
				writer.Indent = num - 1;
				writer.Write("}");
				num = writer.Indent;
				writer.Indent = num - 1;
			}
			writer.WriteLine(");");
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0002A354 File Offset: 0x00028554
		protected virtual void Generate(MoveTableOperation moveTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<MoveTableOperation>(moveTableOperation, "moveTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("MoveTable(name: ");
			writer.Write(this.Quote(moveTableOperation.Name));
			writer.Write(", newSchema: ");
			writer.Write(string.IsNullOrWhiteSpace(moveTableOperation.NewSchema) ? "null" : this.Quote(moveTableOperation.NewSchema));
			writer.WriteLine(");");
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0002A3D4 File Offset: 0x000285D4
		protected virtual void Generate(MoveProcedureOperation moveProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<MoveProcedureOperation>(moveProcedureOperation, "moveProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("MoveStoredProcedure(name: ");
			writer.Write(this.Quote(moveProcedureOperation.Name));
			writer.Write(", newSchema: ");
			writer.Write(string.IsNullOrWhiteSpace(moveProcedureOperation.NewSchema) ? "null" : this.Quote(moveProcedureOperation.NewSchema));
			writer.WriteLine(");");
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0002A454 File Offset: 0x00028654
		protected virtual void Generate(RenameTableOperation renameTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameTableOperation>(renameTableOperation, "renameTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameTable(name: ");
			writer.Write(this.Quote(renameTableOperation.Name));
			writer.Write(", newName: ");
			writer.Write(this.Quote(renameTableOperation.NewName));
			writer.WriteLine(");");
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0002A4C0 File Offset: 0x000286C0
		protected virtual void Generate(RenameProcedureOperation renameProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameProcedureOperation>(renameProcedureOperation, "renameProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameStoredProcedure(name: ");
			writer.Write(this.Quote(renameProcedureOperation.Name));
			writer.Write(", newName: ");
			writer.Write(this.Quote(renameProcedureOperation.NewName));
			writer.WriteLine(");");
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0002A52C File Offset: 0x0002872C
		protected virtual void Generate(RenameColumnOperation renameColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameColumnOperation>(renameColumnOperation, "renameColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameColumn(table: ");
			writer.Write(this.Quote(renameColumnOperation.Table));
			writer.Write(", name: ");
			writer.Write(this.Quote(renameColumnOperation.Name));
			writer.Write(", newName: ");
			writer.Write(this.Quote(renameColumnOperation.NewName));
			writer.WriteLine(");");
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0002A5B4 File Offset: 0x000287B4
		protected virtual void Generate(RenameIndexOperation renameIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameIndexOperation>(renameIndexOperation, "renameIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameIndex(table: ");
			writer.Write(this.Quote(renameIndexOperation.Table));
			writer.Write(", name: ");
			writer.Write(this.Quote(renameIndexOperation.Name));
			writer.Write(", newName: ");
			writer.Write(this.Quote(renameIndexOperation.NewName));
			writer.WriteLine(");");
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0002A63C File Offset: 0x0002883C
		protected virtual void Generate(SqlOperation sqlOperation, IndentedTextWriter writer)
		{
			Check.NotNull<SqlOperation>(sqlOperation, "sqlOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("Sql(@");
			writer.Write(this.Quote(sqlOperation.Sql));
			if (sqlOperation.SuppressTransaction)
			{
				writer.Write(", suppressTransaction: true");
			}
			writer.WriteLine(");");
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0002A69C File Offset: 0x0002889C
		protected virtual string ScrubName(string name)
		{
			Check.NotEmpty(name, "name");
			name = new Regex("[^\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Nd}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Cf}\\p{Pc}\\p{Lm}]").Replace(name, string.Empty);
			using (CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider())
			{
				if ((!char.IsLetter(name[0]) && name[0] != '_') || !csharpCodeProvider.IsValidIdentifier(name))
				{
					name = "_" + name;
				}
			}
			return name;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0002A720 File Offset: 0x00028920
		protected virtual string TranslateColumnType(PrimitiveTypeKind primitiveTypeKind)
		{
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Int16:
				return "Short";
			case PrimitiveTypeKind.Int32:
				return "Int";
			case PrimitiveTypeKind.Int64:
				return "Long";
			default:
				return Enum.GetName(typeof(PrimitiveTypeKind), primitiveTypeKind);
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0002A760 File Offset: 0x00028960
		protected virtual string Quote(string identifier)
		{
			return "\"" + identifier + "\"";
		}

		// Token: 0x040008D3 RID: 2259
		private IEnumerable<Tuple<CreateTableOperation, AddForeignKeyOperation>> _newTableForeignKeys;

		// Token: 0x040008D4 RID: 2260
		private IEnumerable<Tuple<CreateTableOperation, CreateIndexOperation>> _newTableIndexes;
	}
}
