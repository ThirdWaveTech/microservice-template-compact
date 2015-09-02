using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Crux.Domain.Entities;
using Crux.NancyFx.Infrastructure.Extensions;
using Nancy;
using NServiceBus;
using __NAME__.App.Domain;
using __NAME__.Messages.Examples;

namespace __NAME__.App.Api
{
    public class ExampleModule : NancyModule
    {
        public ExampleModule(IRepositoryOfId<int> repository, IMappingEngine engine, ISendOnlyBus bus)
        {
            Get["/examples"] = _ => repository.Query<ExampleEntity>()
                .Project().To<ExampleModel>()
                .ToList();

            Get["/example/{id:int}"] = _ => {
                var entity = repository.Load<ExampleEntity>(_.id);
                return engine.Map<ExampleEntity, ExampleModel>(entity);
            };

            Post["/examples"] = _ => {
                var model = this.BindAndValidateModel<NewExampleModel>();

                var entity = new ExampleEntity(model.Name);
                repository.Save(entity);

                return new NewExampleCreatedModel { Id = entity.ID };
            };

            Post["/examples/close"] = _ => {
                var model = this.BindAndValidateModel<CloseExampleModel>();
                bus.Send(new CloseExampleCommand {Id = model.Id});
                return HttpStatusCode.OK;
            };

            Delete["/example/{id:int}"] = _ => {
                repository.Delete<ExampleEntity>(_.id);
                return HttpStatusCode.OK;
            };
        }
    }
}