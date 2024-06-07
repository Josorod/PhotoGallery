import { Component, OnInit } from '@angular/core';
import { AlbumService } from '../services/album.service';
import { Album } from '../model/album';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class AlbumComponent implements OnInit {
  albums: Album[] = [];
  selectedAlbum: Album | null = null;
  albumForm: FormGroup;
  submitted = false;
  error: string | null = null;

  constructor(private albumService: AlbumService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.loadAlbums();

    this.albumForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      description: ['', [Validators.maxLength(200)]]
    });
  }
  get f() {
    return this.albumForm.controls;
  }

  loadAlbums(): void {
    this.albumService.getAlbums().subscribe({
      next: (data) => this.albums = data,
      error: (err) => this.error = err
    });
  }

  selectAlbum(album: Album): void {
    this.selectedAlbum = album;
    this.albumForm.patchValue(album);
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.albumForm.invalid) {
      return;
    }

    if (this.selectedAlbum) {
      this.updateAlbum();
    } else {
      this.createAlbum();
    }
  }

  createAlbum(): void {
    const { name, description } = this.albumForm.value;
    this.albumService.createAlbum(name, description).subscribe({
      next: (data) => {
        this.albums.push(data);
        this.resetForm();
      },
      error: (err) => this.error = err
    });
  }

  updateAlbum(): void {
    if (!this.selectedAlbum) return;

    const { id } = this.selectedAlbum;
    const { name, description } = this.albumForm.value;
    this.albumService.updateAlbum(id, name, description).subscribe({
      next: (data) => {
        const index = this.albums.findIndex(album => album.id === id);
        if (index !== -1) {
          this.albums[index] = data;
        }
        this.resetForm();
      },
      error: (err) => this.error = err
    });
  }

  deleteAlbum(id: number): void {
    this.albumService.deleteAlbum(id).subscribe({
      next: () => {
        this.albums = this.albums.filter(album => album.id !== id);
      },
      error: (err) => this.error = err
    });
  }

  resetForm(): void {
    this.selectedAlbum = null;
    this.submitted = false;
    this.albumForm.reset();
  }
}
