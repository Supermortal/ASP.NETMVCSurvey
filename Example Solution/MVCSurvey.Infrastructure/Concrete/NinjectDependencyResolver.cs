using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MVCSurvey.Infrastructure.Abstract.Survey;
using MVCSurvey.Infrastructure.Concrete.Survey;
using Ninject;

namespace MVCSurvey.Infrastructure.Concrete
{
    public class NinjectDependencyResolver : IDependencyResolver
    {

        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ISurveyModelRepository>().To<EFSurveyModelRepository>();
            kernel.Bind<ISurveyKeyValueRepository>().To<EFSurveyKeyValueRepository>();
            kernel.Bind<ISurveyParameterRepository>().To<EFSurveyParameterRepository>();
            kernel.Bind<ISurveyParameterMemberRepository>().To<EFSurveyParameterMemberRepository>();
            kernel.Bind<ISurveyInstanceRepository>().To<EFSurveyInstanceRepository>();
            kernel.Bind<ISurveyService>().To<EFSurveyService>();
        }

    }
}