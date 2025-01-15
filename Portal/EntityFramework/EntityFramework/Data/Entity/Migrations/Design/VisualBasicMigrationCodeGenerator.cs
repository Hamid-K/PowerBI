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
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualBasic;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020000E8 RID: 232
	public class VisualBasicMigrationCodeGenerator : MigrationCodeGenerator
	{
		// Token: 0x06001179 RID: 4473 RVA: 0x0002B028 File Offset: 0x00029228
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
				from cfk in operations.OfType<CreateIndexOperation>()
				where ct.Name.EqualsIgnoreCase(cfk.Table)
				select Tuple.Create<CreateTableOperation, CreateIndexOperation>(ct, cfk)).ToList<Tuple<CreateTableOperation, CreateIndexOperation>>();
			ScaffoldedMigration scaffoldedMigration = new ScaffoldedMigration
			{
				MigrationId = migrationId,
				Language = "vb",
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

		// Token: 0x0600117A RID: 4474 RVA: 0x0002B210 File Offset: 0x00029410
		protected virtual string Generate(IEnumerable<MigrationOperation> operations, string @namespace, string className)
		{
			Check.NotNull<IEnumerable<MigrationOperation>>(operations, "operations");
			Check.NotEmpty(className, "className");
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (IndentedTextWriter writer = new IndentedTextWriter(stringWriter))
				{
					this.WriteClassStart(@namespace, className, writer, "Inherits DbMigration", false, this.GetNamespaces(operations));
					writer.WriteLine("Public Overrides Sub Up()");
					IndentedTextWriter writer5 = writer;
					int num = writer5.Indent;
					writer5.Indent = num + 1;
					operations.Except(this._newTableForeignKeys.Select((Tuple<CreateTableOperation, AddForeignKeyOperation> t) => t.Item2)).Except(this._newTableIndexes.Select((Tuple<CreateTableOperation, CreateIndexOperation> t) => t.Item2)).Each(delegate(dynamic o)
					{
						if (VisualBasicMigrationCodeGenerator.<>o__3.<>p__0 == null)
						{
							VisualBasicMigrationCodeGenerator.<>o__3.<>p__0 = CallSite<Action<CallSite, VisualBasicMigrationCodeGenerator, object, IndentedTextWriter>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "Generate", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						VisualBasicMigrationCodeGenerator.<>o__3.<>p__0.Target(VisualBasicMigrationCodeGenerator.<>o__3.<>p__0, this, o, writer);
					});
					IndentedTextWriter writer2 = writer;
					num = writer2.Indent;
					writer2.Indent = num - 1;
					writer.WriteLine("End Sub");
					writer.WriteLine();
					writer.WriteLine("Public Overrides Sub Down()");
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
						if (VisualBasicMigrationCodeGenerator.<>o__3.<>p__1 == null)
						{
							VisualBasicMigrationCodeGenerator.<>o__3.<>p__1 = CallSite<Action<CallSite, VisualBasicMigrationCodeGenerator, object, IndentedTextWriter>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "Generate", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						VisualBasicMigrationCodeGenerator.<>o__3.<>p__1.Target(VisualBasicMigrationCodeGenerator.<>o__3.<>p__1, this, o, writer);
					});
					if (flag)
					{
						writer.Write("Throw New NotSupportedException(");
						writer.Write(this.Generate(Strings.ScaffoldSprocInDownNotSupported));
						writer.WriteLine(")");
					}
					IndentedTextWriter writer4 = writer;
					num = writer4.Indent;
					writer4.Indent = num - 1;
					writer.WriteLine("End Sub");
					this.WriteClassEnd(@namespace, writer);
				}
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0002B4D4 File Offset: 0x000296D4
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
					indentedTextWriter.WriteLine("' <auto-generated />");
					this.WriteClassStart(@namespace, className, indentedTextWriter, "Implements IMigrationMetadata", true, null);
					indentedTextWriter.Write("Private ReadOnly Resources As New ResourceManager(GetType(");
					indentedTextWriter.Write(className);
					indentedTextWriter.WriteLine("))");
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

		// Token: 0x0600117C RID: 4476 RVA: 0x0002B5E8 File Offset: 0x000297E8
		protected virtual void WriteProperty(string name, string value, IndentedTextWriter writer)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("Private ReadOnly Property IMigrationMetadata_");
			writer.Write(name);
			writer.Write("() As String Implements IMigrationMetadata.");
			writer.WriteLine(name);
			int num = writer.Indent;
			writer.Indent = num + 1;
			writer.WriteLine("Get");
			num = writer.Indent;
			writer.Indent = num + 1;
			writer.Write("Return ");
			writer.WriteLine(value ?? "Nothing");
			num = writer.Indent;
			writer.Indent = num - 1;
			writer.WriteLine("End Get");
			num = writer.Indent;
			writer.Indent = num - 1;
			writer.WriteLine("End Property");
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0002B6AD File Offset: 0x000298AD
		protected virtual void WriteClassAttributes(IndentedTextWriter writer, bool designer)
		{
			if (designer)
			{
				writer.WriteLine("<GeneratedCode(\"EntityFramework.Migrations\", \"{0}\")>", typeof(VisualBasicMigrationCodeGenerator).Assembly().GetInformationalVersion());
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0002B6D4 File Offset: 0x000298D4
		protected virtual void WriteClassStart(string @namespace, string className, IndentedTextWriter writer, string @base, bool designer = false, IEnumerable<string> namespaces = null)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			Check.NotEmpty(className, "className");
			Check.NotEmpty(@base, "base");
			(namespaces ?? this.GetDefaultNamespaces(designer)).Each(delegate(string n)
			{
				writer.WriteLine("Imports " + n);
			});
			if (!designer)
			{
				writer.WriteLine("Imports Microsoft.VisualBasic");
			}
			writer.WriteLine();
			int num;
			if (!string.IsNullOrWhiteSpace(@namespace))
			{
				writer.Write("Namespace ");
				writer.WriteLine(@namespace);
				IndentedTextWriter writer2 = writer;
				num = writer2.Indent;
				writer2.Indent = num + 1;
			}
			this.WriteClassAttributes(writer, designer);
			writer.Write("Public ");
			if (designer)
			{
				writer.Write("NotInheritable ");
			}
			writer.Write("Partial Class ");
			writer.Write(className);
			writer.WriteLine();
			IndentedTextWriter writer3 = writer;
			num = writer3.Indent;
			writer3.Indent = num + 1;
			writer.WriteLine(@base);
			IndentedTextWriter writer4 = writer;
			num = writer4.Indent;
			writer4.Indent = num - 1;
			writer.WriteLine();
			IndentedTextWriter writer5 = writer;
			num = writer5.Indent;
			writer5.Indent = num + 1;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0002B840 File Offset: 0x00029A40
		protected virtual void WriteClassEnd(string @namespace, IndentedTextWriter writer)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			int num = writer.Indent;
			writer.Indent = num - 1;
			writer.WriteLine("End Class");
			if (!string.IsNullOrWhiteSpace(@namespace))
			{
				num = writer.Indent;
				writer.Indent = num - 1;
				writer.WriteLine("End Namespace");
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0002B898 File Offset: 0x00029A98
		protected virtual void Generate(AddColumnOperation addColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddColumnOperation>(addColumnOperation, "addColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AddColumn(");
			writer.Write(this.Quote(addColumnOperation.Table));
			writer.Write(", ");
			writer.Write(this.Quote(addColumnOperation.Column.Name));
			writer.Write(", Function(c)");
			this.Generate(addColumnOperation.Column, writer, false);
			writer.WriteLine(")");
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0002B920 File Offset: 0x00029B20
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
				writer.Write("removedAnnotations := ");
				this.GenerateAnnotations(dropColumnOperation.RemovedAnnotations, writer);
				num = writer.Indent;
				writer.Indent = num - 1;
			}
			writer.WriteLine(")");
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0002B9DC File Offset: 0x00029BDC
		protected virtual void Generate(AlterColumnOperation alterColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AlterColumnOperation>(alterColumnOperation, "alterColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AlterColumn(");
			writer.Write(this.Quote(alterColumnOperation.Table));
			writer.Write(", ");
			writer.Write(this.Quote(alterColumnOperation.Column.Name));
			writer.Write(", Function(c)");
			this.Generate(alterColumnOperation.Column, writer, false);
			writer.WriteLine(")");
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0002BA64 File Offset: 0x00029C64
		protected internal virtual void GenerateAnnotations(IDictionary<string, object> annotations, IndentedTextWriter writer)
		{
			Check.NotNull<IDictionary<string, object>>(annotations, "annotations");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("New Dictionary(Of String, Object)() From _");
			writer.WriteLine("{");
			int num = writer.Indent;
			writer.Indent = num + 1;
			string[] array = annotations.Keys.OrderBy((string k) => k).ToArray<string>();
			for (int i = 0; i < array.Length; i++)
			{
				writer.Write("{ ");
				writer.Write(this.Quote(array[i]) + ", ");
				this.GenerateAnnotation(array[i], annotations[array[i]], writer);
				writer.WriteLine((i < array.Length - 1) ? " }," : " }");
			}
			num = writer.Indent;
			writer.Indent = num - 1;
			writer.Write("}");
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0002BB5C File Offset: 0x00029D5C
		protected internal virtual void GenerateAnnotations(IDictionary<string, AnnotationValues> annotations, IndentedTextWriter writer)
		{
			Check.NotNull<IDictionary<string, AnnotationValues>>(annotations, "annotations");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("New Dictionary(Of String, AnnotationValues)() From _");
			writer.WriteLine("{");
			int num = writer.Indent;
			writer.Indent = num + 1;
			if (annotations != null)
			{
				string[] array = annotations.Keys.OrderBy((string k) => k).ToArray<string>();
				for (int i = 0; i < array.Length; i++)
				{
					writer.WriteLine("{");
					num = writer.Indent;
					writer.Indent = num + 1;
					writer.WriteLine(this.Quote(array[i]) + ",");
					writer.Write("New AnnotationValues(oldValue := ");
					this.GenerateAnnotation(array[i], annotations[array[i]].OldValue, writer);
					writer.Write(", newValue := ");
					this.GenerateAnnotation(array[i], annotations[array[i]].NewValue, writer);
					writer.WriteLine(")");
					num = writer.Indent;
					writer.Indent = num - 1;
					writer.WriteLine((i < array.Length - 1) ? " }," : " }");
				}
			}
			num = writer.Indent;
			writer.Indent = num - 1;
			writer.Write("}");
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0002BCBC File Offset: 0x00029EBC
		protected internal virtual void GenerateAnnotation(string name, object annotation, IndentedTextWriter writer)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			if (annotation == null)
			{
				writer.Write("Nothing");
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

		// Token: 0x06001186 RID: 4486 RVA: 0x0002BD24 File Offset: 0x00029F24
		protected virtual void Generate(CreateProcedureOperation createProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateProcedureOperation>(createProcedureOperation, "createProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			this.Generate(createProcedureOperation, "CreateStoredProcedure", writer);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0002BD4B File Offset: 0x00029F4B
		protected virtual void Generate(AlterProcedureOperation alterProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AlterProcedureOperation>(alterProcedureOperation, "alterProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			this.Generate(alterProcedureOperation, "AlterStoredProcedure", writer);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0002BD74 File Offset: 0x00029F74
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
				writer.WriteLine("Function(p) New With");
				IndentedTextWriter writer3 = writer;
				num = writer3.Indent;
				writer3.Indent = num + 1;
				writer.WriteLine("{");
				IndentedTextWriter writer4 = writer;
				num = writer4.Indent;
				writer4.Indent = num + 1;
				procedureOperation.Parameters.Each(delegate(ParameterModel p, int i)
				{
					string text2 = this.ScrubName(p.Name);
					writer.Write(".");
					writer.Write(text2);
					writer.Write(" =");
					this.Generate(p, writer, !string.Equals(p.Name, text2, StringComparison.Ordinal));
					if (i < procedureOperation.Parameters.Count - 1)
					{
						writer.Write(",");
					}
					writer.WriteLine();
				});
				IndentedTextWriter writer5 = writer;
				num = writer5.Indent;
				writer5.Indent = num - 1;
				writer.WriteLine("},");
				IndentedTextWriter writer6 = writer;
				num = writer6.Indent;
				writer6.Indent = num - 1;
			}
			writer.Write("body :=");
			if (!string.IsNullOrWhiteSpace(procedureOperation.BodySql))
			{
				writer.WriteLine();
				IndentedTextWriter writer7 = writer;
				num = writer7.Indent;
				writer7.Indent = num + 1;
				string text = "\" & vbCrLf & _" + writer.NewLine + writer.CurrentIndentation() + "\"";
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
			writer.WriteLine(")");
			writer.WriteLine();
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0002BF98 File Offset: 0x0002A198
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
				list.Add("name := " + this.Quote(parameterModel.Name));
			}
			if (parameterModel.MaxLength != null)
			{
				list.Add("maxLength := " + parameterModel.MaxLength.ToString());
			}
			if (parameterModel.Precision != null)
			{
				list.Add("precision := " + parameterModel.Precision.ToString());
			}
			if (parameterModel.Scale != null)
			{
				list.Add("scale := " + parameterModel.Scale.ToString());
			}
			if (parameterModel.IsFixedLength != null)
			{
				list.Add("fixedLength := " + parameterModel.IsFixedLength.ToString().ToLowerInvariant());
			}
			if (parameterModel.IsUnicode != null)
			{
				list.Add("unicode := " + parameterModel.IsUnicode.ToString().ToLowerInvariant());
			}
			if (parameterModel.DefaultValue != null)
			{
				if (VisualBasicMigrationCodeGenerator.<>o__18.<>p__2 == null)
				{
					VisualBasicMigrationCodeGenerator.<>o__18.<>p__2 = CallSite<Action<CallSite, List<string>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, List<string>, object> target = VisualBasicMigrationCodeGenerator.<>o__18.<>p__2.Target;
				CallSite <>p__ = VisualBasicMigrationCodeGenerator.<>o__18.<>p__2;
				List<string> list2 = list;
				if (VisualBasicMigrationCodeGenerator.<>o__18.<>p__1 == null)
				{
					VisualBasicMigrationCodeGenerator.<>o__18.<>p__1 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, string, object, object> target2 = VisualBasicMigrationCodeGenerator.<>o__18.<>p__1.Target;
				CallSite <>p__2 = VisualBasicMigrationCodeGenerator.<>o__18.<>p__1;
				string text = "defaultValue := ";
				if (VisualBasicMigrationCodeGenerator.<>o__18.<>p__0 == null)
				{
					VisualBasicMigrationCodeGenerator.<>o__18.<>p__0 = CallSite<Func<CallSite, VisualBasicMigrationCodeGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__, list2, target2(<>p__2, text, VisualBasicMigrationCodeGenerator.<>o__18.<>p__0.Target(VisualBasicMigrationCodeGenerator.<>o__18.<>p__0, this, parameterModel.DefaultValue)));
			}
			if (!string.IsNullOrWhiteSpace(parameterModel.DefaultValueSql))
			{
				list.Add("defaultValueSql := " + this.Quote(parameterModel.DefaultValueSql));
			}
			if (!string.IsNullOrWhiteSpace(parameterModel.StoreType))
			{
				list.Add("storeType := " + this.Quote(parameterModel.StoreType));
			}
			if (parameterModel.IsOutParameter)
			{
				list.Add("outParameter := True");
			}
			writer.Write(list.Join(null, ", "));
			writer.Write(")");
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0002C2B0 File Offset: 0x0002A4B0
		protected virtual void Generate(DropProcedureOperation dropProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropProcedureOperation>(dropProcedureOperation, "dropProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropStoredProcedure(");
			writer.Write(this.Quote(dropProcedureOperation.Name));
			writer.WriteLine(")");
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0002C300 File Offset: 0x0002A500
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
			writer.WriteLine("Function(c) New With");
			IndentedTextWriter writer3 = writer;
			num = writer3.Indent;
			writer3.Indent = num + 1;
			writer.WriteLine("{");
			IndentedTextWriter writer4 = writer;
			num = writer4.Indent;
			writer4.Indent = num + 1;
			int columnCount = createTableOperation.Columns.Count<ColumnModel>();
			createTableOperation.Columns.Each(delegate(ColumnModel c, int i)
			{
				string text = this.ScrubName(c.Name);
				writer.Write(".");
				writer.Write(text);
				writer.Write(" =");
				this.Generate(c, writer, !string.Equals(c.Name, text, StringComparison.Ordinal));
				if (i < columnCount - 1)
				{
					writer.Write(",");
				}
				writer.WriteLine();
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
				writer.Write("annotations := ");
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
			writer.WriteLine();
			IndentedTextWriter writer7 = writer;
			num = writer7.Indent;
			writer7.Indent = num - 1;
			writer.WriteLine();
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0002C53C File Offset: 0x0002A73C
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
			writer.WriteLine("Function(c) New With");
			IndentedTextWriter writer3 = writer;
			num = writer3.Indent;
			writer3.Indent = num + 1;
			writer.WriteLine("{");
			IndentedTextWriter writer4 = writer;
			num = writer4.Indent;
			writer4.Indent = num + 1;
			int columnCount = alterTableOperation.Columns.Count<ColumnModel>();
			alterTableOperation.Columns.Each(delegate(ColumnModel c, int i)
			{
				string text = this.ScrubName(c.Name);
				writer.Write(".");
				writer.Write(text);
				writer.Write(" =");
				this.Generate(c, writer, !string.Equals(c.Name, text, StringComparison.Ordinal));
				if (i < columnCount - 1)
				{
					writer.Write(",");
				}
				writer.WriteLine();
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
				writer.Write("annotations := ");
				this.GenerateAnnotations(alterTableOperation.Annotations, writer);
			}
			writer.Write(")");
			writer.WriteLine();
			IndentedTextWriter writer7 = writer;
			num = writer7.Indent;
			writer7.Indent = num - 1;
			writer.WriteLine();
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0002C6EC File Offset: 0x0002A8EC
		protected virtual void GenerateInline(AddPrimaryKeyOperation addPrimaryKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			if (addPrimaryKeyOperation != null)
			{
				writer.WriteLine(" _");
				writer.Write(".PrimaryKey(");
				this.Generate(addPrimaryKeyOperation.Columns, writer);
				if (!addPrimaryKeyOperation.HasDefaultName)
				{
					writer.Write(", name := ");
					writer.Write(this.Quote(addPrimaryKeyOperation.Name));
				}
				if (!addPrimaryKeyOperation.IsClustered)
				{
					writer.Write(", clustered := False");
				}
				writer.Write(")");
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0002C770 File Offset: 0x0002A970
		protected virtual void GenerateInline(AddForeignKeyOperation addForeignKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddForeignKeyOperation>(addForeignKeyOperation, "addForeignKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine(" _");
			writer.Write(".ForeignKey(" + this.Quote(addForeignKeyOperation.PrincipalTable) + ", ");
			this.Generate(addForeignKeyOperation.DependentColumns, writer);
			if (addForeignKeyOperation.CascadeDelete)
			{
				writer.Write(", cascadeDelete := True");
			}
			writer.Write(")");
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0002C7EC File Offset: 0x0002A9EC
		protected virtual void GenerateInline(CreateIndexOperation createIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateIndexOperation>(createIndexOperation, "createIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine(" _");
			writer.Write(".Index(");
			this.Generate(createIndexOperation.Columns, writer);
			this.WriteIndexParameters(createIndexOperation, writer);
			writer.Write(")");
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0002C848 File Offset: 0x0002AA48
		protected virtual void Generate(IEnumerable<string> columns, IndentedTextWriter writer)
		{
			Check.NotNull<IEnumerable<string>>(columns, "columns");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("Function(t) ");
			if (columns.Count<string>() == 1)
			{
				writer.Write("t." + this.ScrubName(columns.Single<string>()));
				return;
			}
			writer.Write("New With { " + columns.Join((string c) => "t." + this.ScrubName(c), ", ") + " }");
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0002C8CC File Offset: 0x0002AACC
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
				writer.Write("New String() { ");
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
					writer.Write("New String() { ");
				}
				writer.Write(addForeignKeyOperation.PrincipalColumns.Join(new Func<string, string>(this.Quote), ", "));
				if (flag)
				{
					writer.Write(" }");
				}
			}
			if (addForeignKeyOperation.CascadeDelete)
			{
				writer.Write(", cascadeDelete := True");
			}
			if (!addForeignKeyOperation.HasDefaultName)
			{
				writer.Write(", name := ");
				writer.Write(this.Quote(addForeignKeyOperation.Name));
			}
			writer.WriteLine(")");
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0002CA20 File Offset: 0x0002AC20
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
					writer.Write("New String() { ");
				}
				writer.Write(dropForeignKeyOperation.DependentColumns.Join(new Func<string, string>(this.Quote), ", "));
				if (flag)
				{
					writer.Write(" }");
				}
				writer.Write(", ");
				writer.Write(this.Quote(dropForeignKeyOperation.PrincipalTable));
			}
			writer.WriteLine(")");
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0002CB00 File Offset: 0x0002AD00
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
				writer.Write("New String() { ");
			}
			writer.Write(addPrimaryKeyOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
			if (flag)
			{
				writer.Write(" }");
			}
			if (!addPrimaryKeyOperation.HasDefaultName)
			{
				writer.Write(", name := ");
				writer.Write(this.Quote(addPrimaryKeyOperation.Name));
			}
			if (!addPrimaryKeyOperation.IsClustered)
			{
				writer.Write(", clustered := False");
			}
			writer.WriteLine(")");
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0002CBDC File Offset: 0x0002ADDC
		protected virtual void Generate(DropPrimaryKeyOperation dropPrimaryKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropPrimaryKeyOperation>(dropPrimaryKeyOperation, "dropPrimaryKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropPrimaryKey(");
			writer.Write(this.Quote(dropPrimaryKeyOperation.Table));
			if (!dropPrimaryKeyOperation.HasDefaultName)
			{
				writer.Write(", name := ");
				writer.Write(this.Quote(dropPrimaryKeyOperation.Name));
			}
			writer.WriteLine(")");
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0002CC50 File Offset: 0x0002AE50
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
				writer.Write("New String() { ");
			}
			writer.Write(createIndexOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
			if (flag)
			{
				writer.Write(" }");
			}
			this.WriteIndexParameters(createIndexOperation, writer);
			writer.WriteLine(")");
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0002CCFC File Offset: 0x0002AEFC
		private void WriteIndexParameters(CreateIndexOperation createIndexOperation, IndentedTextWriter writer)
		{
			if (createIndexOperation.IsUnique)
			{
				writer.Write(", unique := True");
			}
			if (createIndexOperation.IsClustered)
			{
				writer.Write(", clustered := True");
			}
			if (!createIndexOperation.HasDefaultName)
			{
				writer.Write(", name := ");
				writer.Write(this.Quote(createIndexOperation.Name));
			}
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0002CD54 File Offset: 0x0002AF54
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
				writer.Write("New String() { ");
				writer.Write(dropIndexOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
				writer.Write(" }");
			}
			writer.WriteLine(")");
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0002CE04 File Offset: 0x0002B004
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
				list.Add("name := " + this.Quote(column.Name));
			}
			bool? isNullable = column.IsNullable;
			bool flag = false;
			if ((isNullable.GetValueOrDefault() == flag) & (isNullable != null))
			{
				list.Add("nullable := False");
			}
			if (column.MaxLength != null)
			{
				list.Add("maxLength := " + column.MaxLength.ToString());
			}
			if (column.Precision != null)
			{
				list.Add("precision := " + column.Precision.ToString());
			}
			if (column.Scale != null)
			{
				list.Add("scale := " + column.Scale.ToString());
			}
			if (column.IsFixedLength != null)
			{
				list.Add("fixedLength := " + column.IsFixedLength.ToString().ToLowerInvariant());
			}
			if (column.IsUnicode != null)
			{
				list.Add("unicode := " + column.IsUnicode.ToString().ToLowerInvariant());
			}
			if (column.IsIdentity)
			{
				list.Add("identity := True");
			}
			if (column.DefaultValue != null)
			{
				if (VisualBasicMigrationCodeGenerator.<>o__33.<>p__2 == null)
				{
					VisualBasicMigrationCodeGenerator.<>o__33.<>p__2 = CallSite<Action<CallSite, List<string>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, List<string>, object> target = VisualBasicMigrationCodeGenerator.<>o__33.<>p__2.Target;
				CallSite <>p__ = VisualBasicMigrationCodeGenerator.<>o__33.<>p__2;
				List<string> list2 = list;
				if (VisualBasicMigrationCodeGenerator.<>o__33.<>p__1 == null)
				{
					VisualBasicMigrationCodeGenerator.<>o__33.<>p__1 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, string, object, object> target2 = VisualBasicMigrationCodeGenerator.<>o__33.<>p__1.Target;
				CallSite <>p__2 = VisualBasicMigrationCodeGenerator.<>o__33.<>p__1;
				string text = "defaultValue := ";
				if (VisualBasicMigrationCodeGenerator.<>o__33.<>p__0 == null)
				{
					VisualBasicMigrationCodeGenerator.<>o__33.<>p__0 = CallSite<Func<CallSite, VisualBasicMigrationCodeGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__, list2, target2(<>p__2, text, VisualBasicMigrationCodeGenerator.<>o__33.<>p__0.Target(VisualBasicMigrationCodeGenerator.<>o__33.<>p__0, this, column.DefaultValue)));
			}
			if (!string.IsNullOrWhiteSpace(column.DefaultValueSql))
			{
				list.Add("defaultValueSql := " + this.Quote(column.DefaultValueSql));
			}
			if (column.IsTimestamp)
			{
				list.Add("timestamp := True");
			}
			if (!string.IsNullOrWhiteSpace(column.StoreType))
			{
				list.Add("storeType := " + this.Quote(column.StoreType));
			}
			writer.Write(list.Join(null, ", "));
			if (column.Annotations.Any<KeyValuePair<string, AnnotationValues>>())
			{
				int num = writer.Indent;
				writer.Indent = num + 1;
				writer.WriteLine(list.Any<string>() ? "," : "");
				writer.Write("annotations := ");
				this.GenerateAnnotations(column.Annotations, writer);
				num = writer.Indent;
				writer.Indent = num - 1;
			}
			writer.Write(")");
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0002D1BE File Offset: 0x0002B3BE
		protected virtual string Generate(byte[] defaultValue)
		{
			return "New Byte() {" + defaultValue.Join(null, ", ") + "}";
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0002D1DC File Offset: 0x0002B3DC
		protected virtual string Generate(DateTime defaultValue)
		{
			return string.Concat(new string[]
			{
				"New DateTime(",
				defaultValue.Ticks.ToString(),
				", DateTimeKind.",
				Enum.GetName(typeof(DateTimeKind), defaultValue.Kind),
				")"
			});
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0002D23C File Offset: 0x0002B43C
		protected virtual string Generate(DateTimeOffset defaultValue)
		{
			return string.Concat(new string[]
			{
				"New DateTimeOffset(",
				defaultValue.Ticks.ToString(),
				", new TimeSpan(",
				defaultValue.Offset.Ticks.ToString(),
				"))"
			});
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0002D298 File Offset: 0x0002B498
		protected virtual string Generate(decimal defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture) + "D";
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0002D2B0 File Offset: 0x0002B4B0
		protected virtual string Generate(Guid defaultValue)
		{
			string text = "New Guid(\"";
			Guid guid = defaultValue;
			return text + guid.ToString() + "\")";
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0002D2DB File Offset: 0x0002B4DB
		protected virtual string Generate(long defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0002D2E9 File Offset: 0x0002B4E9
		protected virtual string Generate(float defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture) + "F";
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0002D301 File Offset: 0x0002B501
		protected virtual string Generate(string defaultValue)
		{
			return this.Quote(defaultValue);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0002D30C File Offset: 0x0002B50C
		protected virtual string Generate(TimeSpan defaultValue)
		{
			return "New TimeSpan(" + defaultValue.Ticks.ToString() + ")";
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0002D337 File Offset: 0x0002B537
		protected virtual string Generate(HierarchyId defaultValue)
		{
			return "New HierarchyId(\"" + ((defaultValue != null) ? defaultValue.ToString() : null) + "\")";
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0002D358 File Offset: 0x0002B558
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

		// Token: 0x060011A4 RID: 4516 RVA: 0x0002D3A4 File Offset: 0x0002B5A4
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

		// Token: 0x060011A5 RID: 4517 RVA: 0x0002D3EE File Offset: 0x0002B5EE
		protected virtual string Generate(object defaultValue)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { defaultValue }).ToLowerInvariant();
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0002D410 File Offset: 0x0002B610
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
				writer.Write("removedAnnotations := ");
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
				writer.Write("removedColumnAnnotations := ");
				writer.WriteLine("New Dictionary(Of String, IDictionary(Of String, Object)) From _");
				writer.WriteLine("{");
				num = writer.Indent;
				writer.Indent = num + 1;
				string[] array = removedColumnAnnotations.Keys.OrderBy((string k) => k).ToArray<string>();
				for (int i = 0; i < array.Length; i++)
				{
					writer.WriteLine("{");
					num = writer.Indent;
					writer.Indent = num + 1;
					writer.WriteLine(this.Quote(array[i]) + ",");
					this.GenerateAnnotations(removedColumnAnnotations[array[i]], writer);
					writer.WriteLine();
					num = writer.Indent;
					writer.Indent = num - 1;
					writer.WriteLine((i < array.Length - 1) ? " }," : " }");
				}
				num = writer.Indent;
				writer.Indent = num - 1;
				writer.Write("}");
				num = writer.Indent;
				writer.Indent = num - 1;
			}
			writer.WriteLine(")");
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0002D5E8 File Offset: 0x0002B7E8
		protected virtual void Generate(MoveTableOperation moveTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<MoveTableOperation>(moveTableOperation, "moveTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("MoveTable(name := ");
			writer.Write(this.Quote(moveTableOperation.Name));
			writer.Write(", newSchema := ");
			writer.Write(string.IsNullOrWhiteSpace(moveTableOperation.NewSchema) ? "Nothing" : this.Quote(moveTableOperation.NewSchema));
			writer.WriteLine(")");
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0002D668 File Offset: 0x0002B868
		protected virtual void Generate(MoveProcedureOperation moveProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<MoveProcedureOperation>(moveProcedureOperation, "moveProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("MoveStoredProcedure(name := ");
			writer.Write(this.Quote(moveProcedureOperation.Name));
			writer.Write(", newSchema := ");
			writer.Write(string.IsNullOrWhiteSpace(moveProcedureOperation.NewSchema) ? "Nothing" : this.Quote(moveProcedureOperation.NewSchema));
			writer.WriteLine(")");
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0002D6E8 File Offset: 0x0002B8E8
		protected virtual void Generate(RenameTableOperation renameTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameTableOperation>(renameTableOperation, "renameTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameTable(name := ");
			writer.Write(this.Quote(renameTableOperation.Name));
			writer.Write(", newName := ");
			writer.Write(this.Quote(renameTableOperation.NewName));
			writer.WriteLine(")");
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0002D754 File Offset: 0x0002B954
		protected virtual void Generate(RenameProcedureOperation renameProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameProcedureOperation>(renameProcedureOperation, "renameProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameStoredProcedure(name := ");
			writer.Write(this.Quote(renameProcedureOperation.Name));
			writer.Write(", newName := ");
			writer.Write(this.Quote(renameProcedureOperation.NewName));
			writer.WriteLine(")");
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0002D7C0 File Offset: 0x0002B9C0
		protected virtual void Generate(RenameColumnOperation renameColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameColumnOperation>(renameColumnOperation, "renameColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameColumn(table := ");
			writer.Write(this.Quote(renameColumnOperation.Table));
			writer.Write(", name := ");
			writer.Write(this.Quote(renameColumnOperation.Name));
			writer.Write(", newName := ");
			writer.Write(this.Quote(renameColumnOperation.NewName));
			writer.WriteLine(")");
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0002D848 File Offset: 0x0002BA48
		protected virtual void Generate(RenameIndexOperation renameIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameIndexOperation>(renameIndexOperation, "renameIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameIndex(table := ");
			writer.Write(this.Quote(renameIndexOperation.Table));
			writer.Write(", name := ");
			writer.Write(this.Quote(renameIndexOperation.Name));
			writer.Write(", newName := ");
			writer.Write(this.Quote(renameIndexOperation.NewName));
			writer.WriteLine(")");
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0002D8D0 File Offset: 0x0002BAD0
		protected virtual void Generate(SqlOperation sqlOperation, IndentedTextWriter writer)
		{
			Check.NotNull<SqlOperation>(sqlOperation, "sqlOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("Sql(");
			writer.Write(this.Quote(sqlOperation.Sql));
			if (sqlOperation.SuppressTransaction)
			{
				writer.Write(", suppressTransaction := True");
			}
			writer.WriteLine(")");
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0002D930 File Offset: 0x0002BB30
		protected virtual string ScrubName(string name)
		{
			Check.NotEmpty(name, "name");
			name = new Regex("[^\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Nd}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Cf}\\p{Pc}\\p{Lm}]").Replace(name, string.Empty);
			using (VBCodeProvider vbcodeProvider = new VBCodeProvider())
			{
				if ((!char.IsLetter(name[0]) && name[0] != '_') || !vbcodeProvider.IsValidIdentifier(name))
				{
					name = "_" + name;
				}
			}
			return name;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0002D9B4 File Offset: 0x0002BBB4
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

		// Token: 0x060011B0 RID: 4528 RVA: 0x0002D9F4 File Offset: 0x0002BBF4
		protected virtual string Quote(string identifier)
		{
			return "\"" + identifier + "\"";
		}

		// Token: 0x040008EA RID: 2282
		private IEnumerable<Tuple<CreateTableOperation, AddForeignKeyOperation>> _newTableForeignKeys;

		// Token: 0x040008EB RID: 2283
		private IEnumerable<Tuple<CreateTableOperation, CreateIndexOperation>> _newTableIndexes;
	}
}
