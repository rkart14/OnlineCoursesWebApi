using MassTransit;
using StudentEnrollProcessor.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationProcessor.CommandConsumers
{
    public class EnrolledStudentResultConsumer : IConsumer<StudentEnrolledResult>
    {
        public async Task Consume(ConsumeContext<StudentEnrolledResult> context)
        {
            await Console
                .Out
                .WriteLineAsync(MessageBuilder.BuildEnrollNotificationMessage(context.Message.StudentEmail, context.Message.EnrollSucceed));
        }
    }

    internal static class MessageBuilder
    {
        public static string BuildEnrollNotificationMessage(string studentEmail, bool succeed)
        {
            return succeed ?
                ($"Unfortunately student with email {studentEmail} can't enroll to the course :(") :
                ($"Student with email {studentEmail} Has been succesfully enrolled to the course! :)");
        }
    }
}
