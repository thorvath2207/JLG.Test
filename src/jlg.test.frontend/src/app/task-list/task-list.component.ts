import { Component, OnInit } from '@angular/core';
import { TaskService, Task } from '../task.service';
import { NgFor, NgIf } from '@angular/common';
import { EditTaskComponent } from '../edit-task/edit-task.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NgFor, NgIf, EditTaskComponent],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css'],
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];
  createMode = false;
  editMode = false;
  taskToEdit: Task = this.getEmptyTask(); // Initialize with a default Task

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks() {
    this.taskService
      .getTasks()
      .subscribe((tasks: Task[]) => (this.tasks = tasks));
  }

  toggleCompletion(task: Task) {
    task.isCompleted = !task.isCompleted;
    this.taskService.updateTask(task).subscribe();
  }

  openEditTask(task: Task): void {
    this.editMode = true;
    this.createMode = false;
    this.taskToEdit = { ...task }; // Copy the task to avoid mutating the original
  }

  openCreateTask() {
    this.createMode = true;
    this.editMode = false;
    this.taskToEdit = this.getEmptyTask();
  }

  handleSave(task: Task): void {
    if (this.editMode) {
      // Update the existing task
      const index = this.tasks.findIndex((t) => t.id === task.id);
      if (index !== -1) {
        this.tasks[index] = task;
      }
    } else {
      // Add a new task
      task.createdAt = new Date().toISOString();
      this.taskService.addTask(task).subscribe(() => {
        this.loadTasks();
      });
    }
    this.closeModal();
  }

  closeModal(): void {
    this.createMode = false;
    this.editMode = false;
    this.taskToEdit = this.getEmptyTask();
  }

  deleteTask(id?: number) {
    if (id) {
      this.taskService.deleteTask(id).subscribe(() => {
        this.tasks = this.tasks.filter((task) => task.id !== id);
      });
    }
  }

  private getEmptyTask(): Task {
    return {
      title: '',
      description: '',
      dueDate: '',
      isCompleted: false,
    };
  }
}
