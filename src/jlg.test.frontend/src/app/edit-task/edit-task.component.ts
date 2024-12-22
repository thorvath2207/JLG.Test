import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Task } from '../task.service'; // Adjust path as necessary
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-edit-task',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.css'], // Optional
})
export class EditTaskComponent {
  @Input() task: Task = {
    title: '',
    description: '',
    dueDate: '',
    isCompleted: false,
    createdAt: ''
  };
  @Output() save = new EventEmitter<Task>();
  @Output() cancel = new EventEmitter<void>();

  onSubmit(): void {
    this.save.emit(this.task);
  }

  onCancel(): void {
    this.cancel.emit();
  }
}
