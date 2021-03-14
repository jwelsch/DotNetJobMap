# DotNetJobMap

A library for decoupling code that uses reflection to route messages to jobs.

## Concept

A job represents a piece of work to complete. They can be as granular as needed. Each job has a corresponding message to which only it can respond. Once a job is performed it returns a message, which is used to select the next job to perform. There is a 1-to-1 relationship between jobs and messages. Each type of job can only respond to one type of message, and each type of message can only correspond to one type of job. The `Controller` class is responsible for keeping a repository of jobs and messages and for routing a message to the appropriate job.

## API

### Controller

Responsible for coordinating messages and jobs. Once messages and jobs are added this class gets the jobs done.

#### Constructors

* `Controller()` - Default constructor that initializes the instance with the default implementation of underlying classes.
* `Controller(IMessage firstMessage, params IJob[] jobs)` - Convenience constructor that uses the default implemenation of underlying classes and intializes the `Controller` with the first message and jobs. This makes it easier to then call `Controller.DoNext()` in a loop later.
* `Controller(IMessageRouter router)` - Allows the caller to use custom implementations of underlying classes. Should not need to be used under normal circumstances.
* `Controller(IMessageRouter router, IMessage firstMessage, params IJob[] jobs)` - Allows the caller to use custom implementations of underlying classes. Also intializes the `Controller` with the first message and jobs. This makes it easier to then call `Controller.DoNext()` in a loop later. Should not need to be used under normal circumstances.

#### Methods

* `void AddJobs(params IJob[] jobs)` - Adds jobs to perform.
* `void RemoveJobs(params IJob[] jobs)` - Removes jobs so they will not be considered to perform.
* `IMessage DoNext(IMessage message = null)` - Peforms the job that takes the specified message. If null is passed in as the message, then the internally stored next message is used to find the job to perform.

### Job<T>

This abstract class represents a piece of work to perform. The generic parameter `T` is the corresponding message class that the job takes. Derive from this class to create your own jobs. Note that `Job<T>` implements the interface `IJob`.

#### Methods

* `IMessage Do(T message)` - Override this with the work to perform. Return an `IMessage` that corresponds to the next piece of work to perform. Return `null` to stop performing jobs altogether.

### IMessage

This interface represents a message to a job. Implementors can contain any information needed to perform the corresponding job.

## Full Example
    using DotNetJobMap;

    namespace Example
    {
        public class Message1 : IMessage
        {
        }

        public class Message2 : IMessage
        {
        }

        public class JobA : Job<Message1>
        {
            protected override IMessage Do(Message1 message)
            {
                return new Message2();
            }
        }

        public class JobB : Job<Message2>
        {
            protected override IMessage Do(Message2 message)
            {
                return null;
            }
        }

        class Program
        {
            static void Main(string[] _)
            {
                var jobs = new IJob[]
                {
                    new JobA(),
                    new JobB()
                };

                var controller = new Controller(new Message1(), jobs);

                IMessage result;

                do
                {
                    result = controller.DoNext();
                }
                while (result != null);
            }
        }
    }
