using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Indexes;
using TrainWithUs.Core.Models;
using YesSql.Sql;

namespace TrainWithUs.Core.Migrations
{
    public class TrainingMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public TrainingMigrations(IContentDefinitionManager contentDefinitionManager) =>
            _contentDefinitionManager = contentDefinitionManager;

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(TrainingPart), part => part
            .Attachable()
            .WithField(nameof(TrainingPart.Exercises), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Exercises")
                .WithSettings(new TextFieldSettings()
                {
                    Hint = "The exercises that the training will contain.",
                    Required = true
                })
                .WithEditor("TextArea")
                )
            );

            _contentDefinitionManager.AlterTypeDefinitionAsync("TrainingPost", type => type
            .Creatable()
            .Listable()
            .Draftable()
            .WithPart(nameof(TrainingPart))
            );

            SchemaBuilder.CreateMapIndexTableAsync<TrainingPostIndex>(
                table => table
                .Column<string>(nameof(TrainingPostIndex.ContentItemID), column => column.WithLength(26))
                .Column<string>(nameof(TrainingPostIndex.Coach))
            );

            SchemaBuilder.AlterTableAsync(nameof(TrainingPostIndex), table => table
                .CreateIndex($"IDX_{nameof(TrainingPostIndex)}_{nameof(TrainingPostIndex.Coach)}", nameof(TrainingPostIndex.Coach   ))
            );

            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(CoachProfilePart), part => part
            .Attachable()
            .WithField(nameof(CoachProfilePart.Introduction), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Introduction")
                .WithSettings(new TextFieldSettings()
                {
                    Hint = "Introduce yourself!",
                    Required = true
                })
                .WithEditor("TextArea")
                )
            );

            _contentDefinitionManager.AlterTypeDefinitionAsync("CoachProfilePost", type => type
            .Creatable()
            .Listable()
            .Draftable()
            .WithPart(nameof(CoachProfilePart))
            );

            SchemaBuilder.CreateMapIndexTableAsync<CoachProfilePartIndex>(
                table => table
                .Column<string>(nameof(CoachProfilePartIndex.ContentItemID), column => column.WithLength(26))
                .Column<string>(nameof(CoachProfilePartIndex.CoachId))
            );

            SchemaBuilder.AlterTableAsync(nameof(CoachProfilePartIndex), table => table
                .CreateIndex($"IDX_{nameof(CoachProfilePartIndex)}_{nameof(CoachProfilePartIndex.CoachId)}", nameof(CoachProfilePartIndex.CoachId))
            );


            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(TrainingSubscriptionPart), part => part
            .Attachable()
            );

            _contentDefinitionManager.AlterTypeDefinitionAsync("TrainingSubsription", type => type
            .Creatable()
            .Listable()
            .Draftable()
            .WithPart(nameof(TrainingSubscriptionPart))
            );


            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(MessagePart), part => part
            .Attachable()
            .WithField(nameof(MessagePart.MessageText), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("MessageText")
                .WithSettings(new TextFieldSettings()
                {
                    Hint = "The text you want to send to someone.",
                    Required = true
                })
                .WithEditor("TextArea")
                )
            );

            _contentDefinitionManager.AlterTypeDefinitionAsync("MessagePost", type => type
            .Creatable()
            .Listable()
            .Draftable()
            .WithPart(nameof(MessagePart))
            );

            SchemaBuilder.CreateMapIndexTableAsync<MessagePostIndex>(
                table => table
                .Column<string>(nameof(MessagePostIndex.ContentItemID), column => column.WithLength(26))
                .Column<string>(nameof(MessagePostIndex.FromUserId))
                .Column<string>(nameof(MessagePostIndex.ToUserId))
                .Column<DateTime>(nameof(MessagePostIndex.SendingTimeUtc))

            );

            SchemaBuilder.AlterTableAsync(nameof(MessagePostIndex), table => table
                .CreateIndex($"IDX_{nameof(MessagePostIndex)}_{nameof(MessagePostIndex.FromUserId)}", nameof(MessagePostIndex.FromUserId))
            );
            SchemaBuilder.AlterTableAsync(nameof(MessagePostIndex), table => table
                .CreateIndex($"IDX_{nameof(MessagePostIndex)}_{nameof(MessagePostIndex.ToUserId)}", nameof(MessagePostIndex.ToUserId))
            );
            SchemaBuilder.AlterTableAsync(nameof(MessagePostIndex), table => table
                .CreateIndex($"IDX_{nameof(MessagePostIndex)}_{nameof(MessagePostIndex.SendingTimeUtc)}", nameof(MessagePostIndex.SendingTimeUtc))
            );
            return 6;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.CreateMapIndexTableAsync<TrainingPostIndex>(
                table => table
                .Column<string>(nameof(TrainingPostIndex.ContentItemID), column => column.WithLength(26))
                .Column<string>(nameof(TrainingPostIndex.Coach))
            );

            SchemaBuilder.AlterTableAsync(nameof(TrainingPostIndex), table => table
                .CreateIndex($"IDX_{nameof(TrainingPostIndex)}_{nameof(TrainingPostIndex.Coach)}", nameof(TrainingPostIndex.Coach))
            );

            return 2;
        }

        public int UpdateFrom2()
        {
            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(CoachProfilePart), part => part
            .Attachable()
            .WithField(nameof(CoachProfilePart.Introduction), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Introduction")
                .WithSettings(new TextFieldSettings()
                {
                    Hint = "Introduce yourself!",
                    Required = true
                })
                .WithEditor("TextArea")
                )
            );

            _contentDefinitionManager.AlterTypeDefinitionAsync("CoachProfilePost", type => type
            .Creatable()
            .Listable()
            .Draftable()
            .WithPart(nameof(CoachProfilePart))
            );

            return 3;
        }

        public int UpdateFrom3()
        {
            SchemaBuilder.CreateMapIndexTableAsync<CoachProfilePartIndex>(
                table => table
                .Column<string>(nameof(CoachProfilePartIndex.ContentItemID), column => column.WithLength(26))
                .Column<string>(nameof(CoachProfilePartIndex.CoachId))
            );

            SchemaBuilder.AlterTableAsync(nameof(CoachProfilePartIndex), table => table
                .CreateIndex($"IDX_{nameof(CoachProfilePartIndex)}_{nameof(CoachProfilePartIndex.CoachId)}", nameof(CoachProfilePartIndex.CoachId))
            );

            return 4;
        }

        public int UpdateFrom4()
        {
            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(TrainingSubscriptionPart), part => part
            .Attachable()
            );

            _contentDefinitionManager.AlterTypeDefinitionAsync("TrainingSubsription", type => type
            .Creatable()
            .Listable()
            .Draftable()
            .WithPart(nameof(TrainingSubscriptionPart))
            );

            SchemaBuilder.CreateMapIndexTableAsync<TrainingSubscriptionPartIndex>(
                table => table
                .Column<string>(nameof(TrainingSubscriptionPartIndex.ContentItemID), column => column.WithLength(26))
                .Column<string>(nameof(TrainingSubscriptionPartIndex.AthleteUserId))
            );

            SchemaBuilder.AlterTableAsync(nameof(TrainingSubscriptionPartIndex), table => table
                .CreateIndex($"IDX_{nameof(TrainingSubscriptionPartIndex)}_{nameof(TrainingSubscriptionPartIndex.AthleteUserId)}", nameof(TrainingSubscriptionPartIndex.AthleteUserId))
            );

            return 5;
        }

        public int UpdateFrom5()
        {
            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(MessagePart), part => part
            .Attachable()
            .WithField(nameof(MessagePart.MessageText), field => field
                .OfType(nameof(TextField))
                .WithDisplayName("MessageText")
                .WithSettings(new TextFieldSettings()
                {
                    Hint = "The text you want to send to someone.",
                    Required = true
                })
                .WithEditor("TextArea")
                )
            );

            _contentDefinitionManager.AlterTypeDefinitionAsync("MessagePost", type => type
            .Creatable()
            .Listable()
            .Draftable()
            .WithPart(nameof(MessagePart))
            );

            SchemaBuilder.CreateMapIndexTableAsync<MessagePostIndex>(
                table => table
                .Column<string>(nameof(MessagePostIndex.ContentItemID), column => column.WithLength(26))
                .Column<string>(nameof(MessagePostIndex.FromUserId))
                .Column<string>(nameof(MessagePostIndex.ToUserId))
                .Column<DateTime>(nameof(MessagePostIndex.SendingTimeUtc))

            );

            SchemaBuilder.AlterTableAsync(nameof(MessagePostIndex), table => table
                .CreateIndex($"IDX_{nameof(MessagePostIndex)}_{nameof(MessagePostIndex.FromUserId)}", nameof(MessagePostIndex.FromUserId))
            );
            SchemaBuilder.AlterTableAsync(nameof(MessagePostIndex), table => table
                .CreateIndex($"IDX_{nameof(MessagePostIndex)}_{nameof(MessagePostIndex.ToUserId)}", nameof(MessagePostIndex.ToUserId))
            ); 
            SchemaBuilder.AlterTableAsync(nameof(MessagePostIndex), table => table
                .CreateIndex($"IDX_{nameof(MessagePostIndex)}_{nameof(MessagePostIndex.SendingTimeUtc)}", nameof(MessagePostIndex.SendingTimeUtc))
            );
            return 6;
        }

    }
}
