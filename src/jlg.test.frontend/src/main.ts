import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { FormsModule } from '@angular/forms';
import { TaskListComponent } from './app/task-list/task-list.component';

bootstrapApplication(TaskListComponent, appConfig)
  .catch((err) => console.error(err));
